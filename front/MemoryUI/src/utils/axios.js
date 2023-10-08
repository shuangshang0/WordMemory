import axios from "axios";
import router from "../router/index.js";
const service = axios.create({
    //baseURL:'https://localhost:7213/',//基准接口
    //baseURL:'http://192.168.1.108:7070/',
    // baseURL:'http://192.168.0.103:7070/',
     baseURL:'http://192.168.43.147:7070/',
     
    timeout:5000,                     //超时时间
    // headers:{
    //     'Authorization':'Bearer '
    // }
})
//请求拦截器 config 请求对象
service.interceptors.request.use(
    config=>{
        const token = localStorage.getItem('token')
        if(token) config.headers.Authorization = 'Bearer '+token;
        return config
    }
    );
//响应拦截器 收到响应之后先执行
service.interceptors.response.use(
    response=>{return response},
    ({response})=>{
        const {status} = response
        if(status ===401){  
            router.push({name:'Login'}); 
            localStorage.removeItem('token');         
            location.reload();                            
    }
        return Promise.reject(error)}
    );
export default service;
