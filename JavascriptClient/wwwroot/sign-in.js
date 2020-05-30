let signInButton = document.getElementById("signin");


function createState() {
  return "SessionValue-bitlonger-bitlonger-bitlonger-"
}

function createNonce() {
  return "NonceValue-bitlonger-bitlonger-bitlonger-"
}

signInButton.addEventListener("click", signIn)
function signIn() {

  let redirectUri = "https://localhost:44600/home/signin";
  let responseType = "id_token token";
  let scope = "openid ApiOne"

  let authUrl = "/connect/authorize/callback" +
                    "?client_id=client_id_js" +
                    "&redirect_uri=" + encodeURIComponent(redirectUri) +
                    "&response_type=" + encodeURIComponent(responseType) +
                    "&scope=" + encodeURIComponent(scope) +
                    "&nonce=" + createNonce() +
                    "&state=" + createState();

  let returnUrl = encodeURIComponent(authUrl);

  //console.log(authUrl)
  //console.log("==========")
  //console.log(returnUrl)

  window.location.href = "https://localhost:44321/Auth/Login?ReturnUrl=" + returnUrl;
}