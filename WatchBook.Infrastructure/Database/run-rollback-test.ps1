#!/usr/bin/env pwsh
# ============================================
# ContentImportService Transaction Test Script
# ============================================
# Purpose: Verify EF Core transaction rollback
# Database: WatchBookDb (MSI\SQL2025)
# ============================================

Write-Host "`n=== ContentImportService Transaction Test ===" -ForegroundColor Cyan

# Test Configuration
$ApiBaseUrl = "https://localhost:5001"
$MovieId_Rollback = 550  # Fight Club - for rollback test
$MovieId_Success = 278   # The Shawshank Redemption - for success test

$SqlServer = "MSI\SQL2025"
$Database = "WatchBookDb"

# ============================================
# STEP 1: Cleanup - Existing test data
# ============================================
Write-Host "`nStep 1: Cleaning up test data..." -ForegroundColor Yellow

$SqlCleanup = @"
USE $Database;
GO
DELETE FROM ContentPeople WHERE ContentId IN (
	SELECT Id FROM Contents WHERE TmdbId IN ($MovieId_Rollback, $MovieId_Success)
);
DELETE FROM ContentGenres WHERE ContentId IN (
	SELECT Id FROM Contents WHERE TmdbId IN ($MovieId_Rollback, $MovieId_Success)
);
DELETE FROM ContentCompanies WHERE ContentId IN (
	SELECT Id FROM Contents WHERE TmdbId IN ($MovieId_Rollback, $MovieId_Success)
);
DELETE FROM ContentCountries WHERE ContentId IN (
	SELECT Id FROM Contents WHERE TmdbId IN ($MovieId_Rollback, $MovieId_Success)
);
DELETE FROM ContentNetworks WHERE ContentId IN (
	SELECT Id FROM Contents WHERE TmdbId IN ($MovieId_Rollback, $MovieId_Success)
);
DELETE FROM Contents WHERE TmdbId IN ($MovieId_Rollback, $MovieId_Success);
GO
"@

Invoke-Sqlcmd -ServerInstance $SqlServer -Database $Database -Query $SqlCleanup -ErrorAction Continue
Write-Host "✓ Cleanup completed" -ForegroundColor Green

# ============================================
# STEP 2: Rollback Test - Exception should occur
# ============================================
Write-Host "`nStep 2: Testing rollback (exception expected)..." -ForegroundColor Yellow

try
{
	$response = Invoke-RestMethod -Uri "$ApiBaseUrl/api/content/import/movie/$MovieId_Rollback" `
		-Method Post `
		-ContentType "application/json" `
		-ErrorAction Stop
	Write-Host "✗ UNEXPECTED: Request succeeded (exception should have been thrown)" -ForegroundColor Red
}
catch
{
	$statusCode = $_.Exception.Response.StatusCode
	if ($statusCode -eq 400)
	{
		Write-Host "✓ Exception caught (HTTP 400 - Bad Request)" -ForegroundColor Green
		Write-Host "  Message: $($_.Exception.Response.Content.ReadAsStringAsync().Result)" -ForegroundColor Gray
	}
	else
	{
		Write-Host "⚠ Unexpected error: $($_.Exception.Message)" -ForegroundColor Yellow
	}
}

# ============================================
# STEP 3: Verify Rollback - No data in DB
# ============================================
Write-Host "`nStep 3: Verifying rollback (contents should be empty)..." -ForegroundColor Yellow

$SqlVerify = @"
USE $Database;
GO
SELECT COUNT(*) as ContentCount FROM Contents WHERE TmdbId = $MovieId_Rollback;
SELECT COUNT(*) as GenreCount FROM ContentGenres WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = $MovieId_Rollback);
SELECT COUNT(*) as CompanyCount FROM ContentCompanies WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = $MovieId_Rollback);
SELECT COUNT(*) as CountryCount FROM ContentCountries WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = $MovieId_Rollback);
SELECT COUNT(*) as PersonCount FROM ContentPeople WHERE ContentId IN (SELECT Id FROM Contents WHERE TmdbId = $MovieId_Rollback);
"@

$result = Invoke-Sqlcmd -ServerInstance $SqlServer -Database $Database -Query $SqlVerify
if ($result[0].Column1 -eq 0 -and $result[1].Column1 -eq 0 -and $result[2].Column1 -eq 0 -and $result[3].Column1 -eq 0 -and $result[4].Column1 -eq 0)
{
	Write-Host "✓ ROLLBACK VERIFIED: No data persisted (all counts = 0)" -ForegroundColor Green
}
else
{
	Write-Host "✗ ROLLBACK FAILED: Data was persisted!" -ForegroundColor Red
	Write-Host "  Contents: $($result[0].Column1), Genres: $($result[1].Column1), Companies: $($result[2].Column1), Countries: $($result[3].Column1), People: $($result[4].Column1)"
}

# ============================================
# STEP 4: Remove throw, test normal import
# ============================================
Write-Host "`nStep 4: Normal import test (after removing throw)..." -ForegroundColor Yellow
Write-Host "  → Remove the throw line from ContentImportService.cs" -ForegroundColor Gray
Write-Host "  → Rebuild project (dotnet build)" -ForegroundColor Gray
Write-Host "  → Uncomment the section below and run again" -ForegroundColor Gray

<# 
# Import second movie
$response = Invoke-RestMethod -Uri "$ApiBaseUrl/api/content/import/movie/$MovieId_Success" `
	-Method Post `
	-ContentType "application/json"

if ($response.success)
{
	Write-Host "✓ Import succeeded" -ForegroundColor Green
	Write-Host "  ContentId: $($response.contentId), Title: $($response.title)" -ForegroundColor Gray
}

# Verify - Data should be in DB
$SqlSuccess = @"
USE $Database;
GO
SELECT COUNT(*) FROM Contents WHERE TmdbId = $MovieId_Success;
"@

$successCount = (Invoke-Sqlcmd -ServerInstance $SqlServer -Database $Database -Query $SqlSuccess)[0].Column1
if ($successCount -gt 0)
{
	Write-Host "✓ SUCCESS VERIFIED: Data persisted in database" -ForegroundColor Green
}
else
{
	Write-Host "✗ FAIL: Data was not persisted" -ForegroundColor Red
}
#>

Write-Host "`n=== Test Complete ===" -ForegroundColor Cyan
