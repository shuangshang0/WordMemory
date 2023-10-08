<template>
  <!-- 用户登录界面 Login -->
  <div class="denglubeijing">
  <!-- 上方导航栏 -->
  <div>
      <van-nav-bar
    title="用户登录"
    left-text="返回"
    left-arrow 
    @click-left="onClickLeft" 
    fixed
  />
  </div>
  <!-- 用户登录表单 -->
  <div class="bge">
    <br/>
    <van-form @submit="onSubmit">
    <van-cell-group inset>
      <van-field
        v-model="username"
        name="username"
        label="用户名"
        placeholder="用户名"
        :rules="[{ required: true, message: '请填写用户名' }]"
      />
      <van-field
        v-model="password"
        type="password"
        name="password"
        label="密码"
        placeholder="密码"
        :rules="[{ required: true, message: '请填写密码' }]"
        autocomplete=“off”
      />
    </van-cell-group>
    <div style="margin: 16px;">
      <van-button round block type="primary" native-type="submit">
        登录
      </van-button>
      <br/>
    </div>
    
  </van-form>
      </div>
  </div>
      </template>  
      <script >  
      import { ref } from 'vue';
      import { useRouter } from 'vue-router';
      import axios from "../../utils/axios.js";
      import { showSuccessToast,showFailToast } from 'vant';
  import 'vant/es/toast/style'
  export default {
    setup() {
      const router = useRouter();//实例化路由
      const username = ref('');
      const password = ref('');
      const onClickLeft = () => router.push('/');
      const onSubmit =async(values) => {
        
        const res =await axios.post('api/Login',values)  //发起post请求
        if(res.data.isSuccess==true){ 
          localStorage.setItem('token', res.data.resMsg);
          showSuccessToast("登录成功！");
         router.push('/welcome')}
        else{ showFailToast("用户名或密码错误，请检查");}      
      };
  
      return {
        username,
        password,
        onSubmit,
        onClickLeft,
      };
    },
  };
      </script>
  <style>
  .bge{
    position:fixed;
    bottom:25vh;
    width: 100%;
    margin:auto;
    }
  .denglubeijing{
    margin:0px;
    height: 100vh;
    background: url(../../image/星空.jpg);
    background-size:100% 100%;
    background-attachment:fixed;
    }
    </style>