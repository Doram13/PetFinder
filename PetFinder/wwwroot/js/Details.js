fetch("api/ImageApi", { method: "GET" })
    .then(function (response) {
        return response.json();
    })
    .then(function (object) {
        return object.map
    })
    .then(function (conv) {
        document.getElementById("map-image").src = "data:image/jpg;base64," + conv;
    })