-- ============================================
-- ContentImportService Rollback Test
-- ============================================
-- Database: WatchBookDb
-- Server: MSI\SQL2025
-- Test: Verify transaction rollback on exception
-- ============================================

-- TARGET TEST ID: Yeni bir TMDb movie ID kullan (örn: 550 - Fight Club)
-- API Call: POST /api/content/import/movie/550
-- Expected: Exception atılıyor (TRANSACTION ROLLBACK TEST)

-- ============================================
-- STEP 1: Rollback Test - Kayıtlar OLMAMALI
-- ============================================

USE WatchBookDb;
GO

-- Test öncesi kayıt sayısı
DECLARE @TestTmdbId INT = 550; -- Fight Club TMDb ID

SELECT 'Contents' as TableName, COUNT(*) as RecordCount 
FROM Contents 
WHERE TmdbId = @TestTmdbId

UNION ALL

SELECT 'ContentGenres', COUNT(*) 
FROM ContentGenres 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @TestTmdbId)

UNION ALL

SELECT 'ContentCompanies', COUNT(*) 
FROM ContentCompanies 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @TestTmdbId)

UNION ALL

SELECT 'ContentCountries', COUNT(*) 
FROM ContentCountries 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @TestTmdbId)

UNION ALL

SELECT 'ContentPeople', COUNT(*) 
FROM ContentPeople 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @TestTmdbId);

GO

-- Detaylı kontrol
SELECT * FROM Contents WHERE TmdbId = 550;
SELECT * FROM ContentGenres WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = 550);
SELECT * FROM ContentCompanies WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = 550);
SELECT * FROM ContentCountries WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = 550);
SELECT * FROM ContentPeople WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = 550);

-- ============================================
-- EXPECTED RESULT AFTER ROLLBACK TEST:
-- Tüm sorgular 0 satır döndürmeli
-- Bu, rollback'in başarılı olduğunu kanıtlar
-- ============================================

-- ============================================
-- STEP 2: Normal Import Test - Kayıtlar OLMALI
-- ============================================
-- (Throw satırı kaldırıldıktan sonra yapılacak)
-- API Call: POST /api/content/import/movie/{DIFFERENT_ID}
-- Expected: 200 OK, content başarıyla import edildi

DECLARE @SuccessTmdbId INT = 278; -- The Shawshank Redemption

SELECT 'Contents' as TableName, COUNT(*) as RecordCount 
FROM Contents 
WHERE TmdbId = @SuccessTmdbId

UNION ALL

SELECT 'ContentGenres', COUNT(*) 
FROM ContentGenres 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @SuccessTmdbId)

UNION ALL

SELECT 'ContentCompanies', COUNT(*) 
FROM ContentCompanies 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @SuccessTmdbId)

UNION ALL

SELECT 'ContentCountries', COUNT(*) 
FROM ContentCountries 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @SuccessTmdbId)

UNION ALL

SELECT 'ContentPeople', COUNT(*) 
FROM ContentPeople 
WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = @SuccessTmdbId);

GO

-- ============================================
-- EXPECTED RESULT AFTER SUCCESS TEST:
-- Contents: 1
-- ContentGenres: >=1 (genreler gelmişse)
-- ContentCompanies: >=1 (production companies gelmişse)
-- ContentCountries: >=1 (production countries gelmişse)
-- ContentPeople: >=1 (cast gelmişse)
-- ============================================

-- ============================================
-- Transaction State Verification
-- ============================================

-- Son import edilen content'leri göster
SELECT TOP 10 Id, TmdbId, Title, ContentType, CreatedAt
FROM Contents
ORDER BY CreatedAt DESC;

-- Total counts by type
SELECT 
	ContentType,
	COUNT(*) as ContentCount
FROM Contents
GROUP BY ContentType
ORDER BY ContentCount DESC;
