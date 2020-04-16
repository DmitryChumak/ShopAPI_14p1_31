document.addEventListener('DOMContentLoaded', function() {
    let loginBtn = document.querySelector('#loginBtn');
    let url = 'http://localhost:5000/api/auth/authenticate';

    loginBtn.addEventListener('click', async function() {
        let login = document.querySelector('#loginForm').value;
        let password = document.querySelector('#passwordForm').value;

        let credentials = {
            'login': login,
            'password': password
        };
       
        let headers = {
            'Content-Type':'application/json'
        }
       
        let errorBlock = document.querySelector('#errorBlock');   
        try
        {
            let response = await fetch(url, { 
                                         method: 'POST',
                                         body: JSON.stringify(credentials),
                                         headers: headers
                                        });
                                        
            if(response.ok){
                let responseJson = await response.json();
                errorBlock.classList.add('d-none');
                window.localStorage.setItem('shopapicredentials',responseJson.data.token);
                errorBlock.classList.remove('d-none');
                errorBlock.classList.remove('alert-danger');
                errorBlock.classList.add('alert-success');
                errorBlock.innerHTML = `Welcome, ${responseJson.data.name} ${responseJson.data.lastname}`;
            }else{
                errorBlock.classList.remove('d-none');
                let responseJson = await response.json();
                errorBlock.innerText = responseJson.message;
            }
        } 
        catch(error)
        {
            errorBlock.classList.remove('d-none');
            errorBlock.classList.add('alert-danger');
            errorBlock.innerHTML = `Unknown error. Failed to connect to service`;
        }  
    });
});