<?php
$conf['db_host'] = "User-PC\SQLEXPRESS";
$conf['db_user'] = "sa";
$conf['db_pass'] = "Pass!";
$conf['db_name'] = "OdinAccounts";

$user = sql_clean($_GET['Username']);
$passhash = sql_clean($_GET['Password']);


$con = mssql_connect($conf['db_host'],$conf['db_user'],$conf['db_pass']) or die('Database connect Fail.');
$db = mssql_select_db($conf['db_name'], $con) or die('Database Init Fail.');

$exec = mssql_query("SELECT nEMID, sUserPass FROM tAccounts where sUsername = '$user'");

if($exec)
{
    if(mssql_num_rows($exec) != 1)
    {
        die('Wrong Username.');
    }
    $AccountData = mssql_fetch_assoc($exec);
    $PlaintxtPass = $AccountData['sUserPass'];
    $PlaintxtnEMID = $AccountData['nEMID'];
    if (MD5($PlaintxtPass) == $passhash)
    {
        $Token = RandomToken(35);

        $setToken = null;

        if (mssql_num_rows(mssql_query("SELECT * FROM tTokens WHERE nEMID = '".$PlaintxtnEMID."'")) >= 1)
        {
            mssql_query("DELETE FROM tTokens WHERE nEMID = '".$PlaintxtnEMID."'");
            $setToken = mssql_query("INSERT INTO tTokens (nEMID, sToken) VALUES('".$PlaintxtnEMID."', '".$Token."')");
        }
        else
            $setToken = mssql_query("INSERT INTO tTokens (nEMID, sToken) VALUES('".$PlaintxtnEMID."', '".$Token."')");

        if ($setToken)
            die('OK#'.$Token);
        else
            die('SetToken Error');
    }
    else
    {
        die('Wrong Password.');
    }
}
else
{
    die('Query Failed');
}

mssql_close();

function sql_clean($str)
{
    $search  = array("\\", "\0", "\n", "\r", "\x1a", "'", '"');
    $replace = array("", "", "", "", "", "", "");
    return str_replace($search, $replace, $str);
}

function RandomToken( $length )
{
	$chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        $str = "";
	$size = strlen( $chars );
	for( $i = 0; $i < $length; $i++ ) {
		$str .= $chars[ rand( 0, $size - 1 ) ];
	}

	return $str;
}
?>