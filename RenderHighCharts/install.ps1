
   param($installPath, $toolsPath, $package, $project)
   $path=Split-Path $project.FileName
$sourceDirectory = "$path\phantomjs"
   foreach ($proj in get-project -all) 
   {   
 
    	$p = Get-Project -Name $($proj.Name) 
   

   if($p.ProjectItems|Where-Object { $_.Name -eq 'App_Data' })
   {
		write-host $($sourceDirectory) 
		$destinationDirectory = Split-Path $proj.FileName
		$App_DataDirectory= join-path -path $destinationDirectory -childPath App_Data  | join-path  -childPath phantomjs\
		write-host $($App_DataDirectory) 
		xcopy $sourceDirectory  $($App_DataDirectory)   /E
    }


   }

