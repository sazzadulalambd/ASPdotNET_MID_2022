function phoneValid() {
    var value = true;
    var data = document.forms["register1"]["phone"].value;
    if (data.length < 11) {
        document.getElementById("phone_error").innerHTML = "Invalid Phone!!";
        return false;
    }
    else {
        document.getElementById("phone_error").innerHTML = "";
        return true;
    }
}

function showPass(mayvale) {
    var x = document.getElementById("passShow");
    if (x.type === "password") {
        x.type = "text";
    }
    else {
        x.type = "password";
    }
}

function nameValid() {
    var n = document.getElementById("fname").value;
    if (n.length < 1) {
        document.getElementById("namer").innerHTML = "Invalid Name!!";
    }
    else {
        document.getElementById("namer").innerHTML = "";
    }
}

function liValid() {
    var n = document.getElementById("liname").value;
    if (n.length < 3) {
        document.getElementById("lierror").innerHTML = "Invalid License!!";
    }
    else {
        document.getElementById("lierror").innerHTML = "";
    }
}

function tinValid() {
    var n = document.getElementById("tin").value;
    if (n.length < 8) {
        document.getElementById("tinerror").innerHTML = "Invalid T.I.N!!";
    }
    else {
        document.getElementById("tinerror").innerHTML = "";
    }
}

function onameValid() {
    var n = document.getElementById("oname").value;
    if (n.length < 1) {
        document.getElementById("ovalid").innerHTML = "Invalid Name!!";
    }
    else {
        document.getElementById("ovalid").innerHTML = "";
    }
}

function lnameValid() {
    var l = document.getElementById("lname").value;
    if (l.length < 1) {
        document.getElementById("namel").innerHTML = "Invalid Name!!";
    }
    else {
        document.getElementById("namel").innerHTML = "";
    }
}

function dobValid() {
    var d = document.getElementById("dob").value;
    const ar = d.split("-");
    if (ar[0] > 2012) {
        document.getElementById("dobe").innerHTML = "Invalid Age";
    }
    else {
        document.getElementById("dobe").innerHTML = "";
    }
}

function passValid() {
    var value = true;
    var pass = document.getElementById("passShow").value;
    console.log(pass.length);
    if (pass.length < 8) {
        document.getElementById("passerror").innerHTML = "Invalid pass!!";
        return false;
    }
    else {
        document.getElementById("passerror").innerHTML = "";
        return true;
    }

}

function phone2() {
    var phn = document.getElementById("phone").value;
    console.log(phn.length);
    if (phn.length < 10) {
        document.getElementById("perror").innerHTML = "Invalid Phone!!";
        return false;
    }
    else {
        document.getElementById("perror").innerHTML = "";
        return true;
    }
}

function openchatForm() {
    document.getElementById("offer").style.display = "none";
    if (document.getElementById("message").style.display == "block") {
        document.getElementById("message").style.display = "none";
    }
    else {
        document.getElementById("message").style.display = "block"
    }

}

function closechatForm() {
    document.getElementById("message").style.display = "none";
}

function openofferForm() {
    document.getElementById("message").style.display = "none";
    if (document.getElementById("offer").style.display == "block") {
        document.getElementById("offer").style.display = "none";
    }
    else {
        document.getElementById("offer").style.display = "block";
    }

}

function jonpassp1() {
    var k = document.getElementById("p1").value;
    if (k.length < 7) {
        document.getElementById("p1error").innerHTML = "Pass Atleast 8!!";
    } else {
        document.getElementById("p1error").innerHTML = "";
    }
}

function jonpassp2() {
    var j = document.getElementById("p2").value;
    var k = document.getElementById("p1").value;

    if (j.match(k)) {
        document.getElementById("p2error").innerHTML = "";
        return true;
    }
    else {
        document.getElementById("p2error").innerHTML = "Pass did not match!!";
        return false;
    }
}

function joinpass() {
    var j = document.getElementById("p2");
    var k = document.getElementById("p1");
    if (j.type === "password") {
        j.type = "text";
        k.type = "text";
    }
    else {
        j.type = "password";
        k.type = "password";
    }

}

function closeofferForm() {
    document.getElementById("offer").style.display = "none";
}

function searchmydata() {
    var data = document.getElementById("searchdata").value;

    var xttp = new XMLHttpRequest();
    xttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("show").innerHTML = this.responseText;
        }
    };
    xttp.open("GET", "/WTproject/Project/control/ajax.php?data=" + data, true);
    // xttp.setRequestHeader("Content-type","applicaton/x-www-form-urlencoded");
    xttp.send();
    console.log(data);
}

function gosearch() {
    // console.log(document.getElementById("show").getElementsByTagName('p')[0].getElementsByTagName['a'][0]);
    var a = document.getElementById("show");
    console.log(a.innerText);
    a.setAttribute('href', "../view/in_entrepreneure.php?search=" + a.innerText);
}

function messagelog() {
    var msg = document.getElementById("msggo").innerText;
    const arr = msg.split(" ");
    console.log(arr[arr.length - 1]);
    var data2 = arr[arr.length - 1];
    var xttp = new XMLHttpRequest();
    xttp.open("GET", "/WTproject/Project/view/in_messenger.php?mid=" + data2, true);
    // xttp.setRequestHeader("Content-type","applicaton/x-www-form-urlencoded");
    xttp.send();
    console.log(data2);
    window.location.href = "/WTproject/Project/view/in_messenger.php";
}

function ShowHide() {
    var x = document.getElementById("namevar");
    console.log("k");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}



