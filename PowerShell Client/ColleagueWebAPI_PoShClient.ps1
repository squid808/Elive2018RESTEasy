#region Authorization

$Credentials = @{
    UserId='username'
    Password='Hunter1'
} | ConvertTo-Json

$RootUri = "http://localhost:57276/"

#Get a new Colleague API Token
function Get-CollApiToken($Uri, $Credentials){
    return Invoke-RestMethod -Method POST `
    -Uri "$Uri/session/login" -Body $Credentials `
    -ContentType "application/json"
}

#create the Colleague API Header with the provided token
function Get-CollApiHeader($Token){
    return @{"X-CustomCredentials"=$Token}
}

#endregion



#region Delivered Examples

#Get a list of buildings - does not require authentication,
# good for testing connectivity
function Get-CollApiBuildings($Uri){
    return Invoke-RestMethod -Method GET -Uri "$Uri/buildings"
}

#Get a list of majors - requires authentication
function Get-Majors($Uri, $Credentials){
    $Token = Get-CollApiToken $Uri $Credentials
    $Header = Get-CollApiHeader $Token

    return Invoke-RestMethod -Method GET -Uri "$Uri/majors" -Headers $Header
}

#endregion



#region GET example

#Get from the example endpoint
function Get-WebAdvisorIdAndDate {
    param (
        $Uri,
        $Credentials,
        [string]$ColleagueId
    )

    $Token = Get-CollApiToken $Uri $Credentials
    $Header = Get-CollApiHeader $Token

    return Invoke-RestMethod -Method GET `
        -Uri "$Uri/custom/WebAdvisorIdAndDate/$ColleagueId" `
        -Headers $Header
}

#endregion



#region POST example

#Post to the example endpoint
function Post-WebAdvisorIdAndDate($Uri, $Credentials, $WaiddObj){
    $Token = Get-CollApiToken $Uri $Credentials
    $Header = Get-CollApiHeader $Token

    Invoke-RestMethod -Method POST -Uri ("$Uri/custom/WebAdvisorIdAndDate") `
        -Headers $Header `
        -Body ($WaiddObj | ConvertTo-Json) `
        -ContentType "application/json"
}

#endregion



<# Example Usage

#TO USE THE GET ENDPOINT
Get-WebAdvisorIdAndDate $RootUri $Credentials 0123456

#TO USE THE POST ENDPOINT
$WaiddObj = New-Object -TypeName psobject -Property @{"ColleagueId"="0123456"}
Post-WebAdvisorIdAndDate $RootUri $Credentials $WaiddObj

#>