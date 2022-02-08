window.addEventListener("load", init);


const urlParams = new URLSearchParams(window.location.search)
const id = parseInt(urlParams.get("langId"))

function init() {
    fetchCurrentLanguage();
    loadUnusedIdes();
}

function fetchCurrentLanguage() {
    fetch(`/api/Languages/${id}`, {
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(lang => fillTable(lang))
        .catch(error => {
            alert(error.message)
        })
}

function fillTable(lang) {
    const div = document.getElementById("idesDiv")
    if (lang.ides.length > 0) {
        div.innerHTML = `
            <h3>Ides</h3>
            <table class="table table-striped mt-4 order-top border">
                <thead>
                    <tr class="bg-secondary text-light">
                        <th>Name</th>
                        <th>Manufacturor</th>
                        <th>Release Date</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody id="tableBody">       
                </tbody>
            </table>
            `
        lang.ides.forEach(ide => addIdeRow(ide))
    }
}

function addIdeRow(ide) {
    const tbody = document.getElementById("tableBody")
    let price;
    if (ide.price === 0)
        price = "Free"
    else
        price = `&euro;` + ide.price;
    tbody.innerHTML +=
        `
        <tr>
            <td>${ide.name}</td>
            <td>${ide.manufacturer}</td>
            <td>${ide.releaseDate}</td>
            <td>${price}
            </td>
        </tr>
        `
}

function loadUnusedIdes() {
    fetch(`/api/Ides/${id}`, {
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(ides => createSelection(ides))
        .catch(error => {
            alert(error.message)
        })
}

function createSelection(ides) {
    const div = document.getElementById("addIdeDiv")
    div.innerHTML =
        `
        <h4>Add Ide to Language</h4>
        <div id="ideSelectDiv" class="row" style="width:95%">
            <label class="col-sm-3" for="ideSelect">Ide</label>
            <select id="ideSelect" class="form-control col-sm-9"></select>
        </div>
        <div  class="row mt-2" style="width:95%">
            <label class="col-sm-3 mt-2" for="popOrder">Popularity order</label>
            <input  id="popOrder" class="form-control col-sm-9" type="number">
        </div>
        <button type="button" class="btn btn-primary mt-2" id="addButton">Add Ide to Language</button>
        `
    document.getElementById("addButton").addEventListener("click", buttonClick)

    if (ides.length !== 0) {
        ides.forEach(ide => addSelectOption(ide))
    } else {
        const select = document.getElementById("ideSelect")
        select.innerHTML +=
            `<option value="-1">No ide left to add to Language.</option>`
        document.getElementById("addButton").disabled = true
        document.getElementById("ideSelect").disabled = true
    }
}

function addSelectOption(ide) {
    const select = document.getElementById("ideSelect")
    select.innerHTML +=
        `<option value="${ide.id}">${ide.name}</option>`
}

function addIdeLanguage() {
    let order = document.getElementById("popOrder").value
    if (order === "")
        order = 0;
    fetch(`/api/IdeLanguages`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify({
            "IdeId": document.getElementById("ideSelect").value,
            "LangId": id,
            "PopOrder": order
        })
    })
        .catch(error => {
            alert(error.message)
        })
}

async function buttonClick() {
    addIdeLanguage();
    await sleep(500)
    init();
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}