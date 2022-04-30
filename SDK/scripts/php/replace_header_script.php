<?php
/*pre_pend header script*/

$outscript = "";
$data = "";
require_once("D:/wamp/www/IGKDEV/Lib/igk/igk_framework.php");

//$folder = IGKIO::GetDir('D:\DRSStudio 9.0 Src\Src\addins\drawing2D\tools\IGK.DrSStudio.Drawing2D.Tools');
$folder = IGKIO::GetDir('D:\DEV\APP\AppGS\Model\Src\core\IGK.GS');

$tab = IGKIO::GetFiles($folder, "/\.cs$/i", true);

header("Content-Type: text/html; charset=utf-8");
foreach($tab as $k=>$v)
{
	if (strstr($v, "obj\\"))continue;
	$data = IGKIO::ReadAllText($v);
	$odata = "";
	
	$data = preg_replace("/(using IGK\.ICore\s*;\s*)+/i", "", $data);
	$data = preg_replace("/(using IGK\.ICore\.Menu\s*;\s*)+/i", "", $data);
	$data = preg_replace("/(using IGK\.ICore\.Codec\s*;\s*)+/i", "", $data);
	$data = preg_replace("/(using IGK\.ICore\.WinUI\s*;\s*)+/i", "", $data);
	$data = preg_replace("/(using IGK\.ICore\.Drawing2D\s*;\s*)+/i", "", $data);
	$data = preg_replace("/(using IGK\.ICore\.Drawing2D.Menu\s*;\s*)+/i", "", $data);
	$data = preg_replace("/(using IGK\.ICore\.Drawing2D\.WinUI\s*;\s*)+/i", "", $data);
	$data = preg_replace("/(using IGK\.ICore\.History\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.ICore\.Configuration\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.ICore\.Settings\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.DrSStudio\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.DrSStudio\.WinUI\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.DrSStudio\.Menu\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.DrSStudio\.Drawing2D\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.DrSStudio\.Drawing2D\.Menu\s*;\s*)+/i","", $data);
	$data = preg_replace("/(using IGK\.DrSStudio\.Drawing2D\.WinUI\s*;\s*)+/i","", $data);
	
	
	// if (preg_match("/(using IGK\.ICore\s*;\s*)+/i",$data))
	// {
		// preg_replace("/(using IGK\.ICore\s*;\s*)+/i", "", $data);
		// igk_wln("foundeed ... ".$v."\n");
	// }
	// else{
		// igk_wl("Not founds ... ".$v."\n");		
	// }
	
//using IGK.DrSStudio.Drawing2D.Menu;
	$odata.=<<<EOF
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;

EOF;
	$odata .= "\r\n".$data;
	//igk_wl($odata);
	IGKIO::WriteToFileAsUtf8WBOM($v, $odata,true);	
	igk_wl($v,".\n\n");
	//break;
}
?>
