window.addEventListener("load", fetchAllSoftware)

const reloadResponsesButton = document.getElementById("refresh");
reloadResponsesButton.addEventListener('click', fetchAllSoftware);

function fetchAllSoftware() {
    fetch("/api/Softwares")
        .then(response => response.json())
        .then(software => showSoftwareInTable(software))
        .catch(error => {
            alert(error.message)
        })
}

function showSoftwareInTable(software) {
    const tableBody = document.getElementById("softwareTableB")
    tableBody.innerHTML = ""
    software.forEach(s => showSoftware(s))
}

function showSoftware(software) {
    const tableBody = document.getElementById("softwareTableB")
    let desc = "N/A";
    if (software.description !== "")
        desc = software.description;
    tableBody.innerHTML += `
        <tr>
            <td>${software.name}</td>
            <td>${desc}</td>
            <td><a class="text-primary" href="/Software/Details?id=${software.id}">Details</a></td>
        </tr>`;
}

