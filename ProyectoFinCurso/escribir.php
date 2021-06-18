<?php
 
$arch = fopen($_POST['archivo'] . '.txt', 'w');
 
$t = $_POST['texto'];
$t = str_replace("/", "", $t);
$t = str_replace(chr(92), "", &t);
 
fwrite ($arch, $t);
fclose($arch);
echo 'ok'
 
?>