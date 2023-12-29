$userSid = (gwmi win32_userprofile | select localpath, sid | where-object {$_.localpath -Match "Grzegorz"} | select-object sid).sid
$currentServiceSettings = sc.exe sdshow "Usługa"
$userSidPermisionToReplace = "(A;;RPWPCR;;;" + $userSid + ")S:"
$newServiceSettings = $currentServiceSettings - replace "S:", $userSidPermisionToReplace
sc.exe sdset "Usługa" $newServiceSettings | out-null