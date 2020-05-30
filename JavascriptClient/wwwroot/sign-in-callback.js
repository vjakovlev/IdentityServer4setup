let extractButton = document.getElementById("extract_tokens");
extractButton.addEventListener("click", extractTokens);

function extractTokens(address) {

  let returnValue = address.split("#")[1];
  let values = returnValue.split("&");

  for (var i = 0; i < values.length; i++) {
    let value = values[i];
    let kvPair = value.split("=");
    localStorage.setItem(kvPair[0], kvPair[1]);
  }

  window.location.href = "index";
}

extractTokens(window.location.href)