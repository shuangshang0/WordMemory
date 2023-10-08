<template>
  <!-- 用户界面 user -->
  <div class="userbj">
    <div class="btnu">
    <van-popover v-model:show="showPopover" theme="dark" :actions="actions" @select="onSelect" placement="bottom-end"> 
  <template #reference>
    <van-button plain hairline type="primary" icon="setting-o" ></van-button>
  </template>
</van-popover>
  </div>
  <br/>
  <div class="carduser">
    <p class="p1">已学习新单词：<strong class="strong">{{ state.userlearn.learnword }}</strong>个</p>
  <p class="p1">已学习的天数：<strong class="strong">{{ state.userlearn.learnday }}</strong>天</p>
</div>
  <van-calendar
  title="学习日历"
  :poppable="false"
  :show-confirm="false"
  :style="{ height: '55vh'}"
  type="single" 
  readonly
  :show-subtitle="false"
  :show-mark="false"
  :min-date="minDate" 
  :max-date="maxDate"

  :formatter="formatter" 
/>
<!-- <van-button round plain hairline type="success" @click="onclick">退出账号</van-button> -->
</div>
</template>

<script lang>
import { ref,reactive } from 'vue';
import { showConfirmDialog , showToast  } from 'vant';
import axios from '../../utils/axios.js';
import { useRouter } from 'vue-router';
import { onMounted} from 'vue';
import 'vant/es/dialog/style';
import 'vant/es/toast/style';
export default {
  setup() { 
    const router = useRouter();
    let Calender;
    let date1 = new Date();
    let dateYear = date1.getFullYear();             //获取年 
    let dateMonth = date1.getMonth();               //获取月  
    let dateDate = date1.getDate();                //获取日
    const showPopover = ref(false);
    const state = reactive({userlearn:{},calender:{}});
   
    onMounted(async() => {
  let res =await axios.get('api/GetUserLearn');
  state.userlearn=res.data[0];
  Calender = await axios.get('api/Calender/GetUserLearn');
    })
    const onclick=()=>showConfirmDialog({
  title: ' 确认退出 ',
  message:"是否确认退出账号",
  
})
  .then(() => {
    //清空用户登录token 跳转登录界面
    localStorage.removeItem('token');
    router.push("/Login");
    // on confirm确定时操作
  })
  .catch(() => {
    // on cancel取消时操作
  });
    
  //设置中的选项功能
  const actions = [
      { text: '退出账号' },
      { text: '修改密码' },
    ];
  const onSelect = (action) => {
     if(action.text=='退出账号')
    {onclick();}
    if(action.text=='修改密码')
    {router.push("/changepwd");}
  }
    
   const formatter =(day)=> {
   // 当前月份的日
   //根据性能影响，性能高定时便可以设短反之设长  1500 1000 
   setTimeout(() => {
    for (let i = 0; i < Calender.data.length; i++) {
      let time = (Calender.data[i].date.split(' '))[0];
  let timeyear = (time.split('/'))[0];
  let timemonth = (time.split('/'))[1];
  let timeday = (time.split('/'))[2];
  if(timeyear==year&&timemonth==month&&timeday==date)
      {
        if(Calender.data[i].learnword!=0)
        day.topInfo="学习:"+Calender.data[i].learnword;
        if(Calender.data[i].reviewword!=0)
        day.bottomInfo="复习:"+Calender.data[i].reviewword;
     }
    }
 }, 600);
   const year =day.date.getFullYear();
   const month = day.date.getMonth() + 1;
   const date = day.date.getDate();  
   return day;  
  }
    return {
      minDate: new Date(2023, 0, 1),
      maxDate: new Date(dateYear, dateMonth, dateDate),
      state,
      Calender,
      showPopover,
      actions,
      formatter,
      onclick,
      onSelect,
    };
  },
};
</script>

<style>

.van-calendar__top-info {
  background: linear-gradient(86deg, rgba(146, 209, 201, 0.5), #aeececfa);
}
.van-calendar__bottom-info {
  background: linear-gradient(86deg, rgba(182, 218, 223, 0.5), #9ec6ebfa);
}
.p1{
  font-size:18px;
}
.btnu{
  position: absolute;right: 8px;top: 8px;
}
.carduser {
    margin: 10px 40px 30px 40px;
    padding: 4px;
    background-color: #fff;
    border-radius: 12px;
    box-shadow: 6px 12px 14px #ebedf0;
    text-align: center;
    font-size: larger;
  }
  .strong{
    font-size:24px;
  }

  .userbj{
        margin:0px;
        height: 100vh;
        background: url(../../image/蓝色线条.jpg);
        background-size:100% 100%;
        background-attachment:fixed;
      }
</style>