<?php
require_once("D:/wamp/www/IGKDEV/Lib/igk/igk_framework.php");
$number = "0.8";

$f = "../src/main/IGK.DrSStudio/Properties/AssemblyInfo.cs";
$fmodel ="../src/main/IGK.DrSStudio/Properties/AssemblyInfoModel.cs";
$str = IGKIO::ReadAllText($fmodel);
$data = IGKIO::ReadAllText(__FILE__);
$ntab = explode(".",$number);
$major = $ntab[0];
$minor = $ntab[1];
$minor +=1;
if ($minor > 9999)
{
	$major +=1;
	$minor = 1;
}
$str = str_replace("%AssemblyFileVersion%", ".".$major.".".$minor, $str);
IGKIO::WriteToFileAsUtf8WBOM($f, $str,true);
igk_wln($f);
igk_wln($str);

if (preg_match("/\\\$number/i", $data))
{
    $data = preg_replace("/(\\\$number = \"([0-9\.]+)\";)/i", "\$number = \"{$major}.{$minor}\";", $data);
    IGKIO::WriteToFile(__FILE__, $data,true);
}

exit(0);
?>