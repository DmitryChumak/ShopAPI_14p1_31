class ProductService{
    
    constructor() {
        this.url = 'http://localhost:5000/api/products';
        this.tableResult = document.querySelector('#tableResult');
        this.token = window.localStorage.getItem('shopapicredentials');
        

        document.querySelector('#logoutBtn').addEventListener('click', function () {
            window.localStorage.removeItem('loggedUserInfo');
            window.localStorage.removeItem('shopapicredentials');
            window.location.reload();

        });

        this.userInfo = parseJwt(this.token);
     


        document.querySelector('#userInfo').innerText = `${this.userInfo.unique_name} ${this.userInfo.role}`;
        document.querySelector('#loginArea').classList.remove('d-none');


        this.getAllProducts();
    }

    showErrorBlock(message) {
        let errorBlock = document.querySelector('#errorBlock');
        errorBlock.classList.remove('d-none');
        errorBlock.innerText = message;
    }

    showTableData(products) {
        let resHtml = "";
        if (this.userInfo.role.includes("Admin") || this.userInfo.role.includes("SuperAdmin")) {

            resHtml = `<table class="table mt-3">
        <thead>
            <tr>
                <th scope="col">Id</th>
                <th scope="col">Name</th>
                <th scope="col">GoodCount</th>
                <th scope="col">CategoryId</th>
            </tr>
        </thead><tbody>`;

            for (let product of products) {
                resHtml += `<tr>
            <td>
               
                <div class="productId">${product.id}</div>

            </td>
            <td>        
                <div class="productName">${product.name}</div>         
            </td>
            <td>        
                <div class="productCount">${product.goodCount}</div>         
            </td>
            <td>        
                <div class="productCount">${product.category.name}</div>         
          </td>
            </tr>`;
            }
        } else if (this.userInfo.role.includes("User")) {
            resHtml = `<table class="table mt-3">
        <thead>
            <tr>
                <th scope="col">Id</th>
                <th scope="col">Name</th>
                <th scope="col">GoodCount</th>
                <th scope="col">CategoryId</th>
            </tr>
        </thead><tbody>`;
            for (let product of products) {
                resHtml += `<tr>
            <td>               
                <div class="productId">${product.id}</div>
            </td>
            <td>        
                <div class="productName">${product.name}</div>         
            </td>
            <td>        
                <div class="productCount">${product.goodCount}</div>         
             </td>
             <td>        
             <div class="productCount">${product.CategoryId}</div>         
            </td>
            </tr>`;
            }
        }

        resHtml += `</tbody></table>`;

        errorBlock.classList.add('d-none');

        this.tableResult.innerHTML = resHtml;
        document.querySelector('#productsTable').classList.remove('d-none');

        


    }

    getAllProducts() {
        fetch(this.url, {
            headers: {
                'Authorization': `bearer ${this.token}`
            }
        })
            .then(x => {
                if (!x.ok) {
                    this.showErrorBlock(x.statusText);
                } else {
                    x.json().then(result => {
                        console.log(result);
                        this.showTableData(result.data);
                    });
                }
               
            })
            .catch(error => {
                this.showErrorBlock('Unknown error. Failed to load data');
            });
    }

    
}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};

document.addEventListener('DOMContentLoaded', function () {

    new ProductService();


});