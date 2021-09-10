function statusChangeCallback(response) {  // Called with the results from FB.getLoginStatus().
    console.log('statusChangeCallback');
    console.log(response);                   // The current login status of the person.
    if (response.status === 'connected') {   // Logged into your webpage and Facebook.
        testAPI();
    } else {                                 // Not logged into your webpage or we are unable to tell.
        document.getElementById('status').innerHTML = 'Please log ' +
            'into this webpage.';
    }
}


function checkLoginState() {
    debugger;         // Called when a person is finished with the Login Button.
    FB.getLoginStatus(function (response) {   // See the onlogin handler
        statusChangeCallback(response);
    });
}


window.fbAsyncInit = function () {
    FB.init({
        appId: '191812809501683',
        cookie: true,                     // Enable cookies to allow the server to access the session.
        xfbml: true,                     // Parse social plugins on this webpage.
        version: 'v10.0'           // Use this Graph API version for this call.
    });


    FB.getLoginStatus(function (response) {   // Called after the JS SDK has been initialized.
        statusChangeCallback(response);        // Returns the login status.
    });
};
function fbLogoutUser() {
    FB.getLoginStatus(function (response) {
        if (response && response.status === 'connected') {
            FB.logout(function (response) {
                document.location.reload();
            });
        }
    });
}
function testAPI() {
    FB.api('/me?fields=id,name,email', function (response) {
        $.ajax({
            url: "/Accounts/LoginFB?id=" + response.id + "&name=" + response.name + "&email=" + response.email,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (value) {
                //window.location.replace("https://localhost:44372")
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    });
}
