<template>
<div class="pagebj">


  <!-- 背词页面 主要功能页面 mainpage -->
<!--搜索栏 有道API -->
<br/>
  <div class="top">
  <van-dropdown-menu>
    <van-dropdown-item v-model="value1" :options="option1" />
    <van-dropdown-item v-model="value2" :options="option2" />
  </van-dropdown-menu>

  <form action="/" >
  <van-search
    v-model="query"
    show-action
    placeholder="请输入想要翻译的内容"
    @search="getword"
    @cancel="onCancel"
  /> 
</form>
<div class="mainphoto">
<van-image
  width="100%"
  height="160px"
  :src="ph1"
/>
</div>
<van-popup v-model:show="show"  position="top"
  :style="{ height: '40%'}" round >
  <br>
 <p class="fy1">原文：</p>
 <p class="fy2">{{query}}</p>
 <p class="fy1">翻译：</p>
 <p class="fy2">{{result}}</p>
</van-popup>

</div>
<br/>
<van-button round type="primary" size="large" class="btncolor" @click="onclick1">背新单词</van-button>
<br/><br/><br/>
<van-button round type="primary" size="large" class="btncolor" @click="onclick2">复习单词</van-button>
<br/><br/><br/>
<van-button round type="primary" size="large" class="btncolor" to="welcome">每日一句</van-button>

<div>


</div>
</div>
</template>

<script>
import { ref } from 'vue';
import { showToast } from 'vant';
import { useRouter } from 'vue-router';
import { jsonp } from 'vue-jsonp';
import MD5 from 'js-md5';
import 'vant/es/dialog/style';
import 'vant/es/toast/style';


export default {
  setup() {
    const router = useRouter();//实例化路由  
    const query = ref('');//待翻译文本
    const show = ref(false);
    const result=ref('');
    const ph1 = (new URL('../../image/yunduo2.jpg',import.meta.url)).href;
    const value1 = ref("auto");
    const value2 = ref("auto");
    const option1 = [
      { text: '自动检测', value: "auto" },
      { text: '中文	', value: "zh" },
      { text: '英语	', value: "en" },
      { text: '日语	', value: "jp" },
      { text: '韩语	', value: "kor"},
      { text: '法语	', value: "fra"},
      { text: '西班牙语	', value: "spa"},
      { text: '俄语	', value: "ru"},
      { text: '德语	', value: "de"},
      { text: '荷兰语	', value: "nl"},
      { text: '粤语	', value: "yue"},
      { text: '文言文	', value: "wyw"},
    ];
    const option2 = [
    { text: '自动检测', value: "auto" },
      { text: '中文	', value: "zh" },
      { text: '英语	', value: "en" },
      { text: '日语	', value: "jp" },
      { text: '韩语	', value: "kor"},
      { text: '法语	', value: "fra"},
      { text: '西班牙语	', value: "spa"},
      { text: '俄语	', value: "ru"},
      { text: '德语	', value: "de"},
      { text: '荷兰语	', value: "nl"},
      { text: '粤语	', value: "yue"},
      { text: '文言文	', value: "wyw"},
    ];


    const onCancel = () => 1;
    //showToast('取消');
    const onclick1 =() =>router.push({
					path: '/learnNew'
				});
        const onclick2 =() =>router.push({
					path: '/review'
				});

const appid = '20230303001584474';
const key = 'dG088U8cyxU_mQwr9bLI';
//const from = 'auto';//源语言
//const to = 'auto';//目标语言

//调用百度翻译文本翻译API
const getword = async(val)=>{
  let from = value1.value;//源语言
  let to = value2.value;//目标语言
  let salt = (new Date).getTime();
  let str1 = appid + val + salt +key;
  let sign = MD5(str1);
const res = await jsonp('http://api.fanyi.baidu.com/api/trans/vip/translate',{
        q: val,
        appid: appid,
        salt: salt,
        from: from,
        to: to,
        sign: sign
    });
    show.value = true;
result.value=res.trans_result[0].dst;
}
    return {
      query,
      ph1,
      show,
      result,
      value1,
      value2,
      option1,
      option2,
      getword,
      onCancel,
      onclick1,
      onclick2,
    };
  },
};
</script>

<style>
.btncolor{
  width: 75%;
}
.fy1{
  font-size:18px;
  text-align: left;
  padding-left: 25px;
  color:#4d72ee
}
.fy2{
  font-size:18px;
}
.mainphoto{
  margin: 0 auto;
}
.pagebj{
  margin:0px;
        height: 100vh;
        background: url(../../image/蓝色线条.jpg);
        background-size:100% 100%;
        background-attachment:fixed;
}
</style>