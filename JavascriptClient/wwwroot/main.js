let config = {
  userStore: new OidcWebStorageStateStore({ store: window.localStorage }),
  authority: "https://localhost:44321/",
  client_id: "client_id_js",
  redirect_uri: "https://localhost:44600/home/signin",
  response_type: "id_token token",
  scope: "openid ApiOne ApiTwo viktor.scope"
}

let userManager = new Oidc.UserManager(config);

let signInButton = document.getElementById("signin");
signInButton.addEventListener("click", signIn)
function signIn() {
  userManager.signinRedirect();
}

userManager.getUser().then(user => {
  console.log(user)
  if (user) {
    axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token; 
  }
})

let callapiButton = document.getElementById("callapi");
callapiButton.addEventListener("click", callApi)
function callApi() {
  axios.get("http://localhost:44901/secret")
    .then(res => console.log(res))
}