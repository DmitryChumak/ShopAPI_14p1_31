function renderRolesTable(res) {
    let tableRows = "";

    for (let role of res) {
        tableRows += `<tr>
                                <td>${role.id}</td>
                                <td>${role.name}</td>
                                <td>
                                    <div class="delBtn" data-id=${role.id}>
                                        <img src="https://img.icons8.com/plasticine/32/000000/trash--v1.png"/>
                                    </div>
                                </td>
                            </tr>`;
    }

    let tableBody = `<table class="table">
                        <thead>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Delete</th>
                        </thead>
                        <tbody>
                            ${tableRows}
                        </tbody>
                    </table>`;

    $('#rolesTable').html(tableBody);
    $('#tableResult').removeClass('d-none');
};

async function loadAllRoles() {
    let url = 'http://localhost:5000/api/roles';
    let token = window.localStorage.getItem('shopapicredentials');
    let headers = {
        'Authorization': `bearer ${token}` 
    }
    let response  = await fetch(url, {
        headers: headers
    });

    if(!response.ok){
        $('#errorBlock').removeClass('d-none');
        $('#errorBlock').html(response.statusText);
    }else{
        let responseJson = await response.json();
        renderRolesTable(responseJson.data);
        $('.delBtn').click(async function () {
            let id = $(this).data('id');
            let url = `http://localhost:5000/api/roles/${id}`;

            await fetch(url, {
                method: 'DELETE'
            });
            loadAllRoles();
        });
    }
}

async function createRole() {
    let url = 'http://localhost:5000/api/roles';
    let newRole = {
        "name": $('#nameForm').val()
    };
    let headers = {
        'Content-Type': 'application/json'
    }
        await fetch(url, {
            method: 'POST',
            body: JSON.stringify(newRole),
            headers: headers
            });
        $('#createRoleModal').modal('hide');
        loadAllRoles();
};

$(document).ready(function () {
    $('#saveChangesBtn').click(function () {
        createRole();
        $('#createRoleModal input').val('');
    });
    loadAllRoles();
});