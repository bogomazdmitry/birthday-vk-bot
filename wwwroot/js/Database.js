let startText = document.getElementById('text-birthday').value;

function EditBirthday(obj, id) {
    let par = obj.parentNode.parentNode;
    let BackDate = par.querySelector('#field-birthday').innerText;
    par.querySelector('#field-btn-edit').innerHTML =
        `<div class="btn-group">
    <button class="btn btn-success" id="btn-edit-save">
        <i class="fa fa-check" aria-hidden="true"></i>
    </button>
    <button class="btn btn-danger" id="btn-edit-unsave">
        <i class="fa fa-times" aria-hidden="true"></i>
    </button>
</div>`;
    par.querySelector('#field-birthday').innerHTML =
        `<input type="data" name="input" id="birthday" required value="` + BackDate + `" />`;

    function Back() {
    par.querySelector('#field-btn-edit').innerHTML =
    `<button class="btn btn-default" id="edit-btn" onclick="EditBirthday(this,` + id + `)">
                <i class='far fa-edit'></i>
            </button>`;
    }

    function Save() {
    Back();
        par.querySelector('#field-birthday').innerText = par.querySelector('#birthday').value;
        sendRequest("DataBase", "EditBirthday", "POST", id + '-' + par.querySelector('#field-birthday').innerText);
    }

    function Unsave() {
    Back();
        par.querySelector('#field-birthday').innerHTML = BackDate;
    }

    par.querySelector('#btn-edit-save').addEventListener('click', Save);
    par.querySelector('#btn-edit-unsave').addEventListener('click', Unsave);
}

function DeleteMember(obj, id) {
    obj.parentNode.parentNode.remove();
    sendRequest("DataBase", "DeleteMember", "POST", id);
}

function SaveTextChanges() {
    startText = document.getElementById('text-birthday').value;
    sendRequest("DataBase", "SaveTextChanges", "POST", startText);
}

function UndoTextChanges() {
    document.getElementById('text-birthday').value = startText;
}

function ClearAll() {
    document.getElementById('database').querySelector('tbody').innerHTML = "";
    sendRequest("DataBase", "ClearAll", "POST");
}

function EmptyIfNull(str) {
    if (str == null)
        return "";
    else
        return str;
}

function AddMember() {
    sendRequest("DataBase", "AddMember", "POST", document.getElementById('text-new-member').value)
        .then((str) => {
            return str.json();
        }).then((str) => {
            var tbody = document.getElementById('main-people');
            JSON.parse(str).forEach((element) => {
                tbody.appendChild(document.createElement('tr')).innerHTML =
                    `
                <td id="field-btn-delete" style="width:5%;">
                    <button class="btn btn-danger btn-sm" type="submit" name="deleteMember" onclick="DeleteMember(this,` + element['id'] + `)">Del</button>
                </td>
                <td id="field-fi" style="width: 30%;">`
                    + element["FI"] +
                    `</td>
                <td id="field-id" style="width: 20%;">`
                    + element['id'] +
                    `</td>
                <td id="field-birthday" style="width: 30%;">`
                    + EmptyIfNull(element['dayOfBirthday'])+
                    `</td>
                <td id="field-btn-edit" align="center" style="width: 15%;">
                    <button class="btn btn-default" id="btn-edit" onclick="EditBirthday(this, ` + element['id'] + `)"><i class='far fa-edit'></i></button>
                </td>
                `;
            });
        });
}

function craftUrl(controller, action) {
    return location.origin + '/' + controller + '/' + action;
}

function sendRequest(controller, action, m3thod, data) {
    return fetch(craftUrl(controller, action), {
    method: m3thod,
        headers: {
    'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
}
