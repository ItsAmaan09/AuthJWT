# Generated by SQL Server Management Studio at 19:23 on 06-05-2021

Import-Module SqlServer
# Set up connection and database SMO objects

$password = "admin@123"
$sqlConnectionString = "Data Source=DESKTOP-QJS3RAE\SQLEXPRESS;Initial Catalog=VMSDev;User ID=sa;Password=$password;MultipleActiveResultSets=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;Packet Size=4096;Application Name=`"Microsoft SQL Server Management Studio`""
$smoDatabase = Get-SqlDatabase -ConnectionString $sqlConnectionString

# If your encryption changes involve keys in Azure Key Vault, uncomment one of the lines below in order to authenticate:
#   * Prompt for a username and password:
#Add-SqlAzureAuthenticationContext -Interactive

#   * Enter a Client ID, Secret, and Tenant ID:
#Add-SqlAzureAuthenticationContext -ClientID '<Client ID>' -Secret '<Secret>' -Tenant '<Tenant ID>'

# Change encryption schema

$encryptionChanges = @()

# Add changes for table [dbo].[mstMeetingAgenda]
$encryptionChanges += New-SqlColumnEncryptionSettings -ColumnName dbo.mstMeetingAgenda.DisplayText -EncryptionType Deterministic -EncryptionKey "CEK_Auto1"

Set-SqlColumnEncryption -ColumnEncryptionSettings $encryptionChanges -InputObject $smoDatabase
