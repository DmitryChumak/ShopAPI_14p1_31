
function renderUsersTable(res) {
    let tableRows = "";

    for (let user of res) {
        tableRows += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.name}</td>
                                <td>${user.lastname}</td>
                                <td>${user.login}</td>
                                <td>${user.role}</td>
                                <td>
                                    <div class="delBtn" data-id=${user.id}>
                                        <img src="https://img.icons8.com/plasticine/32/000000/trash--v1.png"/>
                                    </div>
                                </td>
                            </tr>
                        `;
    }

    let tableBody = `
                    <table class="table">
                        <thead>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Lastname</th>
                            <th>Login</th>
                            <th>Roles</th>
                            <th>Delete</th>
                        </thead>
                        <tbody>
                            ${tableRows}
                        </tbody>
                    </table>
                    `;

    $('#usersTable').html(tableBody);
    $('#tableResult').removeClass('d-none');
};

async function loadAllUsers() {
    let url = 'http://localhost:5000/api/users';
    let token = window.localStorage.getItem('shopapicredentials');

    let response = await fetch(url, {
        headers: {
            'Authorization': `bearer ${token}`          
        }
    });
    if (!response.ok) {
        $('#errorBlock').removeClass('d-none');
        $('#errorBlock').html(response.statusText);
    } else {
        let responseJson = await response.json();
        renderUsersTable(responseJson.data);
        $('.delBtn').click(async function () {
            let id = $(this).data('id');
            let url = `http://localhost:5000/api/users/${id}`;
            await fetch(url, {
                        method: 'DELETE'
                       });
            loadAllUsers();
        });
    }
}

async function createUser() {
    let url = 'http://localhost:5000/api/users';
    let newUser = {
        "name": $('#nameForm').val(),
        "lastname": $('#lastnameForm').val(),
        "login": $('#loginForm').val(),
        "password": $('#passwordForm').val(),
        "role": $('#roleForm').val()
    };

    await fetch(url, {
        method: 'POST',
        body: JSON.stringify(newUser),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    $('#createUserModal').modal('hide');
    loadAllUsers();
};

$(document).ready(function () {
    $('#saveChangesBtn').click(function () {
        createUser();
        $('#createUserModal input').val('');
    });
    loadAllUsers();
});