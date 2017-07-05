<script>
    var to_check=$(this).val();
    var cur_string=$("#0").text();
    var to_chk = "that";
    var cur_str= "that";

    if(to_chk==cur_str){
        alert("both are equal");
        $("#0").attr("class","correct");    
    } else {
        alert("both are not equal");
        $("#0").attr("class","incorrect");
    }
</script>