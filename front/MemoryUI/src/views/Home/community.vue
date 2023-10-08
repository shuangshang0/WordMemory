<template>

<van-nav-bar title="交流栏" fixed placeholder/>
  <div class="combj">
  <van-pull-refresh v-model="loading" @refresh="onRefresh">
    <div class="commenb" v-for="item in state.commsg" :key="item">
      <div class="commena">
        <van-swipe-cell>
            <p class="left1">用户<strong style="font-size:1.25rem; ">{{item.username}}:</strong></p>
            <p class="right1" >{{item.msg}}</p>
        </van-swipe-cell>
      </div>
    </div>
  </van-pull-refresh>
  <br/>
  <van-back-top right="3vw" bottom="38vh" />


<!-- 留言按钮和留言内容框 -->
  <van-sticky :offset-bottom="350" position="bottom">
  <van-button  round type="success" icon="chat-o" class="btnmsg" @click="showfield"></van-button>
  </van-sticky>
  <div v-show="open">
    <van-sticky :offset-bottom="100" position="bottom">
  <van-cell-group inset>
  <van-field
    v-model="msg"
    rows="1"
    label="留言"
    type="textarea"
    placeholder="请输入留言"
  />
  </van-cell-group>
  <van-button type="primary" size="small" @click="onclick">发送</van-button>
  </van-sticky>
  </div>
</div>
</template>

<script>
import { ref , inject ,reactive , onMounted} from 'vue';
import axios from '../../utils/axios.js'
import { showSuccessToast,showFailToast,showToast } from 'vant';
import 'vant/es/toast/style'
export default {
  setup() {
    const state = reactive({commsg:{}});
    const msg =ref('');
    const open=ref(false);
    const loading = ref(false);
    const refresh = inject("reload");
    onMounted (async() => {
        let res = await axios.get('api/MsgBoard');
        state.commsg=res.data;
    })

    const onRefresh = () => {
      setTimeout(async () => {
        showToast('刷新成功');
        let res = await axios.get('api/MsgBoard');
        state.commsg=res.data;
        loading.value = false;
      }, 1000);
    };

    const showfield =()=>{
      open.value=!open.value;
    }


    const getNowTime = () => {
      var date = new Date();
      //年 getFullYear()：四位数字返回年份
      var year = date.getFullYear(); //getFullYear()代替getYear()
      //月 getMonth()：0 ~ 11
      var month = date.getMonth() + 1;
      //日 getDate()：(1 ~ 31)
      var day = date.getDate();
      //时 getHours()：(0 ~ 23)
      var hour = date.getHours()<10?"0"+date.getHours():date.getHours();
      //分 getMinutes()： (0 ~ 59)
      var minute = date.getMinutes()<10?"0"+date.getMinutes():date.getMinutes();
      //秒 getSeconds()：(0 ~ 59)
      var second = date.getSeconds()<10?"0"+date.getSeconds():date.getSeconds();
      var time =  year + '-' +month + '-' + day + ' ' + hour + ':' + minute + ':' + second;
      return time;
    }
    const onclick = async() => {
    const date = getNowTime(); 
    if(msg.value.length<100){
      const res = await axios.post('api/MsgBoard',{msg:msg.value,date:date});
      if(res.data.isSuccess==true)
      {
 //shuaxing
      refresh();
        msg.value="";
      showSuccessToast(res.data.resMsg);
      }
      else
      {
        msg.value="";
        showFailToast("出现错误！");}
      }
    else{
      showFailToast("留言长度不能超过100个字符！");
    }
    }

    return {
      open,
      state,
      msg,
      refresh,
      loading,
      onRefresh,
      onclick,
      showfield,
    };
  },
};

</script>

<style>
  .left1{
    margin: 0;
  }
  .right1{
    margin-top:0 ;
    margin-bottom: 15px;
  }
.commena{
  font-size:1.1rem; 
}
.commenb{
  margin: 0px 15px 0 15px;
   text-align: left;
   overflow-wrap: break-word;
}
.btnmsg{
  margin-left:80vw;
}
.combj{
  /* width: 98.5vw; */
  background: url(../../image/淡蓝水珠.jpg);
}
</style>