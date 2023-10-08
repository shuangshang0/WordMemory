<template>
    <!-- 用户注册界面 register -->
  <div class="gaimibj">
    <div>
      <van-nav-bar
    title="修改密码"
    left-text="返回"
    left-arrow 
    @click-left="onClickLeft" 
    fixed
    placeholder
  />
  </div>
  <!-- 用户注册表单 -->
  <div class="bg">
    <br/>
    <van-form @submit="onSubmit">
    <van-cell-group inset>
      <van-field
        v-model="password"
        type="password"
        name="password"
        label="旧密码"
        placeholder="旧密码"
        :rules="[{ required: true, message: '请填写旧密码' }]"
        autocomplete=“off”
      />
      <van-field
        v-model="newpassword"
        type="password"
        name="newpassword"
        label="新密码"
        placeholder="新密码"
        :rules="[{ required: true, message: '请填写新密码' }]"
        autocomplete=“off”
      />
      <van-field
        v-model="confirmpassword"
        type="password"
        name="confirmpassword"
        label="确认新密码"
        placeholder="确认新密码"
        :rules="[{ required: true, message: '请填写相同的新密码' }]"
        autocomplete=“off”
      />
    </van-cell-group>
    <div style="margin: 16px;">
      <van-button round block type="primary" native-type="submit">
        修改
      </van-button>
    </div>
  </van-form>
  <br/>
      </div>
  </div>
  </template>
  
  <script>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  import axios from "../../utils/axios.js";
  import { showSuccessToast,showFailToast } from 'vant';
  import 'vant/es/toast/style'
  export default {
    setup() {
      const router = useRouter();
      const password = ref('');
      const newpassword = ref('');
      const confirmpassword = ref('')
      const onClickLeft = () => history.back();
      const onSubmit =async(values) => {
          if(values.newpassword !== values.confirmpassword){
            showFailToast("请确认两次密码是否输入一致！");
      }
      else {
        const res =await axios.post('api/ChangePwd',values)  //发起post请求   
        if(res.data.isSuccess==true)
        {
          showSuccessToast(res.data.resMsg);
          localStorage.removeItem('token');
          router.push("/Login");
        }
        else
        {
          password.value="";
          newpassword.value="";
          confirmpassword.value="";
          showFailToast(res.data.resMsg);
        }
    }
      };
  
      return {
        password,
        newpassword,
        confirmpassword,
        onSubmit,
        onClickLeft,
      };
    },
  };
  </script>
  
<style>
  .bg{
    position:fixed;
    width: 100%;
    bottom:30vh;
    }
  .gaimibj{
    margin:0px;
    height: 100vh;
    background: url(../../image/星空.jpg);
    background-size:100% 100%;
    background-attachment:fixed;
    }
</style>