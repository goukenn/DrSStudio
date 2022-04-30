<?php
/*pre_pend header script*/

$outscript = "";
$data = "";
require_once("D:/wamp/www/IGKDEV/Lib/igk/igk_framework.php");

//$folder = IGKIO::GetDir('D:\DRSStudio 9.0 Src\Src\addins\drawing2D\tools\IGK.DrSStudio.Drawing2D.Tools');
$folder = IGKIO::GetDir('D:\DRSStudio 9.0 Src\Src');

$tab = IGKIO::GetFiles($folder, "/\.cs$/i", true);

header("Content-Type: text/html; charset=utf-8");
foreach($tab as $k=>$v)
{
	if (strstr($v, "obj\\"))
		continue;
	$data = IGKIO::ReadAllText($v);
	$odata = "";
	
	$data = str_replace("/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CoreLog.cs
*/
"
,"", $data);

$data = str_replace("/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : ICore
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE \"License.txt\"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/"
,
"", $data);

	// if (preg_match("/(using IGK\.ICore\s*;\s*)+/i",$data))
	// {
		// preg_replace("/(using IGK\.ICore\s*;\s*)+/i", "", $data);
		// igk_wln("foundeed ... ".$v."\n");
	// }
	// else{
		// igk_wl("Not founds ... ".$v."\n");		
	// }
	
//using IGK.DrSStudio.Drawing2D.Menu;
$file = basename($v);
	$odata.=<<<EOF
/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: {$file}
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
EOF;
	$odata ="";
	$odata .= "\r\n".$data;
	//igk_wl($odata);
	IGKIO::WriteToFileAsUtf8WBOM($v, $odata,true);	
	igk_wl($v,".\r\n\r\n");
	//break;
}
?>
