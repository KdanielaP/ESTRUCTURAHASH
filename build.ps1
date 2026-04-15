$ErrorActionPreference = "Stop"
Write-Host "Building RegistroVacunas..."
dotnet build
if ($LASTEXITCODE -eq 0) {
    Write-Host "Build successful! Running..."
    dotnet run
} else {
    Write-Host "Build failed. Check errors above."
}
