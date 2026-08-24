# Transaction Rollback Test Guide

## Test Senaryosu
Bu dokümantasyon, ContentImportService'teki transaction mekanizmasının doğru çalıştığını kontrol etmek için yapılacak test adımlarını içerir.

## Sistem Konfigürasyonu
- **Database**: WatchBookDb (Server: MSI\SQL2025)
- **Connection**: Trusted_Connection=True
- **Transaction Type**: Database.BeginTransactionAsync (EF Core explicit transaction)

## Test Ortamı
1. Visual Studio'da projeyi çalıştır (F5)
2. Swagger/API tester aç: `https://localhost:5001/swagger`
3. ContentController'a erişim sağla

## Adım 1: Rollback Test (Exception Case)

### API Çağrısı
```
POST /api/content/import/movie/{tmdbId}
```

**Geçici Throw Kodu** (SaveChangesAsync'ten sonra, CommitAsync'ten önce):
```csharp
throw new InvalidOperationException("TRANSACTION ROLLBACK TEST - Content should not be persisted");
```

### Beklenen Davranış
1. ContentImportService.ImportMovieAsync() çağrılır
2. TMDb'den movie verisi çekilir
3. Genre, Company, Country, Person vb. sync edilir
4. SaveChangesAsync() ilk kez çağrılır (tüm entities DbContext'e eklenir)
5. CommitAsync'ten önceki throw'a çıkılır
6. Catch bloğundaki RollbackAsync() çağrılır
7. Exception tekrar fırlatılır ve client'a gönderilir
8. **Veritabanında KEINE kayıt oluşur (transaction rollback)**

### Veritabanı Doğrulaması
```sql
-- Test öncesi aşağıdaki sorguyu çalıştır
USE WatchBookDb;
GO

-- Kontrol: Test ID'si ile Content kaydı olması gerekmiyor
SELECT COUNT(*) as ContentCount 
FROM Contents 
WHERE TmdbId = {test_tmdb_id};

-- Detaylı kontrol: Tüm ilişkili tablolara bak
SELECT * FROM Contents WHERE TmdbId = {test_tmdb_id};
SELECT * FROM ContentGenres WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {test_tmdb_id});
SELECT * FROM ContentCompanies WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {test_tmdb_id});
SELECT * FROM ContentCountries WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {test_tmdb_id});
SELECT * FROM ContentPeople WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {test_tmdb_id});
```

**Beklenen Sonuç**: Tüm sorgular 0 satır döndürür (rollback başarılı)

---

## Adım 2: Normal Import Test (Success Case)

### Geçici Throw Kodunu Kaldır
ContentImportService.cs içinde aşağıdaki satırı sil:
```csharp
// TEST: Rollback verification - throw exception before commit
throw new InvalidOperationException("TRANSACTION ROLLBACK TEST - Content should not be persisted");
```

### API Çağrısı
```
POST /api/content/import/movie/{tmdbId}
```

Farklı bir TMDb movie ID'si kullan (rollback test'teki ile aynı olmasın)

### Beklenen Davranış
1. ContentImportService.ImportMovieAsync() çağrılır
2. TMDb'den movie verisi çekilir
3. Genre, Company, Country, Person vb. sync edilir
4. SaveChangesAsync() çağrılır
5. CommitAsync() çağrılır - **Transaction commit edilir**
6. Content ve tüm ilişkili kayıtlar veritabanına kaydedilir
7. Success response dönülür

### Veritabanı Doğrulaması
```sql
USE WatchBookDb;
GO

-- Kontrol: Test ID'si ile Content kaydı OLMALI
SELECT COUNT(*) as ContentCount 
FROM Contents 
WHERE TmdbId = {success_tmdb_id};

-- Detaylı kontrol: İlişkili kayıtları kontrol et
SELECT * FROM Contents WHERE TmdbId = {success_tmdb_id};
SELECT COUNT(*) FROM ContentGenres WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {success_tmdb_id});
SELECT COUNT(*) FROM ContentCompanies WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {success_tmdb_id});
SELECT COUNT(*) FROM ContentCountries WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {success_tmdb_id});
SELECT COUNT(*) FROM ContentPeople WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = {success_tmdb_id});
```

**Beklenen Sonuç**: 
- Content kaydı 1 satır
- İlişkili tablolarda >=1 satır (cast/crew/genre/company vb. olduğu sürece)

---

## Test Sonuçları Özeti

| Senaryo | Expected Result | Doğrulama |
|---------|-----------------|-----------|
| Rollback Test | Database'de KEINE kayıt | SELECT COUNT(*) = 0 |
| Normal Import | Database'de 1 Content + ilişkiler | SELECT COUNT(*) >= 1 |
| Atomicity | Kısmi kayıt YOK (all or nothing) | Tüm ilişkili tablolar tutarlı |

---

## EF Core Transaction Yapısı

```csharp
using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
{
	try
	{
		// 1. Tüm entity operasyonları (Add/Update/Delete)
		_dbContext.Genres.Add(...);
		_dbContext.Companies.Add(...);
		// ...

		// 2. SaveChangesAsync - Database'e yazma (transaction içinde)
		_dbContext.Contents.Add(content);
		await _dbContext.SaveChangesAsync(cancellationToken);

		// 3. CommitAsync - Transaction'ı finalize et
		await transaction.CommitAsync(cancellationToken);
	}
	catch
	{
		// 4. Exception durumunda - Rollback yapılır
		await transaction.RollbackAsync(cancellationToken);
		throw;
	}
}
```

### Önemli Noktalar:
1. **BeginTransactionAsync**: Explicit transaction başlatır
2. **SaveChangesAsync**: Transaction kapsamında SaveChanges yapılır
3. **CommitAsync**: Transaction confirm edilir
4. **RollbackAsync**: Exception durumunda tüm değişiklikler geri alınır
5. **Using statement**: Transaction resource'ları otomatik dispose edilir

