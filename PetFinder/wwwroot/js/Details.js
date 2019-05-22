let url = window.location.protocol+ "://" + window.location.hostname +":"+ window.location.port + "/api/ImageApi";

fetch(url, { method: "GET" })
    .then(function (response) {
        console.log(response);
    //    return response.json();
    })
    //.then(function (conv) {
    //    document.getElementById("map-image").src = "data:image/jpg;base64," + conv;
    //})