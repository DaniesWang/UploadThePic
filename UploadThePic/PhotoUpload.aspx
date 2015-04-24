<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoUpload.aspx.cs" Inherits="UploadThePic.PhotoUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上传图片</title>
    <script type="text/javascript" src="http://ycdoit.com/js/jquery-1.11.1.min.js"></script>
    
</head>
<body>
        <!--
        大家注意到这个form的target的了么？这个target属性的值frameFile,是form之后的iframe的name值,
        这样的写法是让当前的form表单在提交表单内容的时候转交给iframe中进行页面中表单处理，
        并且不会产生当前页面跳转!
     -->
       <img width='200' height='200' id='imgShow' alt='' src="/images/no_pic.gif" />
        <form id='formFile' name='formFile' method="post" action="/handler/PhotoUploadHandler.ashx" target='frameFile'
            enctype="multipart/form-data">
            <input type='file' id='fileUp' name='fileUp' />
            <div id='uploadLog'> </div>
            <br />
        </form>
        <!--
        这个iframe拿到post过来的表单数据后会开始在自身内部访问post过来的页面地址,在内部中它会刷新页面，
        但是这已不重要了，因为当前的iframe已经被我display:none隐藏了！所以这样给用户看起来像是无刷新的
        页面文件上传，其实只是做一个一个小小的技巧！
    -->
        <iframe id='frameFile' name='frameFile' style='display: none;'></iframe>
    <script type="text/javascript">

        $('#fileUp').change(function () {
            $('#uploadLog').html('开始上传中....');
            $('#formFile').submit();
        });

        function uploadSuccess(msg) {
            if (msg.split('|').length > 1) {
                $('#imgShow').attr('src', msg.split('|')[1]);
                $('#uploadLog').html(msg.split('|')[0]);
            } else {
                $('#uploadLog').html(msg);
            }
        }
    </script>
</body>
</html>
