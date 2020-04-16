
function renderUsersTable(users, roles) {
    let tableRows = "";
    let token = window.localStorage.getItem('shopapicredentials');

    for (let user of users) {
        let rolesBody = "";

        for (let role of roles) {
            let checked = user.role.includes(role.name) ? "checked" : "";
            rolesBody += `<div class="form-check">
                            <input class="roleId" type="hidden" value="${role.id}" />
                            <label class="form-check-label">
                            <input type="checkbox" class="form-check-input roleCheck" ${checked} /> 
                            <span>${role.name}</span>
                        </label>
                    </div>`
        }
        tableRows += `<tr>
                        <td class="userId">${user.id}</td>
                        <td>${user.login}</td>
                        <td><div>${rolesBody}</div></td>
                        <td
                      </tr>`;
    }

    let tableBody = `<table class="table">
                        <thead>
                            <th>Id</th>
                            <th>Login</th>
                            <th>Roles</th>
                        </thead>
                        <tbody>
                            ${tableRows}
                        </tbody>
                    </table>`;

    $('#userRolesTable').html(tableBody);
    $('#userRolesTable').removeClass('d-none');
    $('.roleCheck').click(async function () {
        let userId = $(this).closest("tr").children(".userId").text();
        let roleId = $(this).parent().prev('.roleId').val();

        let setUrl = 'http://localhost:5000/api/userroles/setrole';
        let deleteUrl = `http://localhost:5000/api/userroles/deleterole/${userId}`;

        let isChecked = $(this).prop('checked');

        if (isChecked) {
            await fetch(setUrl, {
                method: 'POST',
                headers: {
                    'Authorization': `bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 'userId': userId, 'roleId': roleId })
            });
            $('.notification').show();
            setTimeout(() => $('.notification').hide(), 2000);
        } else {
            await fetch(deleteUrl, {
                method: 'DELETE',
                headers: {
                    'Authorization': `bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 'roleId': roleId })
            });
            $('.notification').show();
            setTimeout(() => $('.notification').hide(), 2000);
        }
    });
};

async function loadAllUsers() {
    let token = window.localStorage.getItem('shopapicredentials');
    let usersUrl = 'http://localhost:5000/api/users';
    let rolesUrl = 'http://localhost:5000/api/roles';
    const headers = {
        'Authorization': `bearer ${token}`
    };
    let response = await fetch(usersUrl, { headers: headers });
    let users = await response.json();
    let rolesResponse = await fetch(rolesUrl, {headers: headers});
    let roles = await rolesResponse.json();
    console.log(roles);
    console.log(users);
    renderUsersTable(users.data, roles.data);
}

$(document).ready(function () {
    loadAllUsers();
});