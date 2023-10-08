<template>
  <!-- 用户注册界面 register -->
<div class="denglubeijing">
  <div>
    <van-nav-bar
  title="用户注册"
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
      v-model="username"
      name="username"
      label="用户名"
      placeholder="用户名"
      :rules="[{ required: true, message: '请填写用户名' },
    { pattern, message: '请输入1-20位的合法字符' }]"
    />
    <van-field
      v-model="password"
      type="password"
      name="password"
      label="密码"
      placeholder="密码"
      :rules="[{ required: true, message: '请填写密码' },
            { pattern, message: '请输入1-20位的合法字符' }]" 
      autocomplete=“off”
    />
    <van-field
      v-model="confirmpassword"
      type="password"
      name="confirmpassword"
      label="确认密码"
      placeholder="确认密码"
      :rules="[{ required: true, message: '请填写相同的密码' },
    { pattern, message: '请输入1-20位的合法字符' }]"
      autocomplete=“off”
    />
    <van-field
      v-model="phone"
      name="phone"
      label="手机号"
      placeholder="手机号"
      :rules="[{ required: true, message: '请填写手机号' },
    { validator,message: '请填写正确的手机号'  }]"
    />
    <van-field
      v-model="email"
      name="email"
      label="邮箱"
      placeholder="邮箱"
      :rules="[{ required: true, message: '请填写邮箱' }]"
    />
  </van-cell-group>
  <div style="margin: 16px;">
    <van-button round block type="primary" native-type="submit">
      注册
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
    const username = ref('');
    const password = ref('');
    const confirmpassword = ref('')
    const phone = ref('');
    const email = ref('');
    //正则校验 1-20位中文英文数字字符
    const pattern = /^[\u4e00-\u9fa5a-zA-Z0-9]{1,20}$/;
    const validator = (val) => /^(13[0-9]|14[01456879]|15[0-35-9]|16[2567]|17[0-8]|18[0-9]|19[0-35-9])\d{8}$/.test(val);
    //const pattern3 = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

    const onClickLeft = () => router.push('/');
    const onSubmit =async(values) => {
        if(values.password !== values.confirmpassword ){
          showFailToast("请确认两次密码是否输入一致！");
    }
    else {
      const res =await axios.post('api/Register',values)
      if(res.data.isSuccess==true)
        { 
          router.push("/Login");
          showSuccessToast(res.data.resMsg+"跳转至登录页面");
        }
        else
        {
          password.value="";
          username.value="";
          confirmpassword.value="";
          showFailToast(res.data.resMsg);
        }
    }
    };

    return {
      username,
      password,
      phone,
      email,
      pattern,
      confirmpassword,
      onSubmit,
      onClickLeft,
      validator,
    };
  },
};
</script>

<style>
.bg{
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