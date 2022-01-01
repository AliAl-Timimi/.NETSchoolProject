window.addEventListener("load", init)

const urlParams = new URLSearchParams(window.location.search)
const id = urlParams.get("id");

function init() {
    document.getElementById("saveButton").addEventListener("click", saveChanges)
    FetchSoftware()
}

function FetchSoftware() {
    fetch(`/api/Softwares/${id}`)
        .then(response => response.json())
        .then(software => fillForm(software))
        .catch(error => {
            alert(error.message)
        })
}

function fillForm(software) {
    document.getElementById("name").value = software.name;
    document.getElementById("desc").value = software.description;
}

function saveChanges() {
    let updatedSoftware = {
        "id": id,
        "name": document.getElementById("name").value,
        "description": document.getElementById("desc").value,
        "languageUsed": null
    }
    fetch(`/api/Softwares/${id}`, {
        method: 'PUT',
        body: JSON.stringify(updatedSoftware),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => {
            let val = document.getElementById("nameVal")
            let suc = document.getElementById("softwareSuccess")
            if (response.status === 400) {
                suc.innerHTML = ""
                val.innerHTML = "Name should be at least 3 characters long"
            } else if (response.status === 204) {
                suc.innerHTML = "Software added successfully!"
                val.innerHTML = ""
            }
        })
        .catch(error => {
            alert(error.message)
        })
}