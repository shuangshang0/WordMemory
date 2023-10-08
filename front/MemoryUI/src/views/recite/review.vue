<template>
  <div id="word-main">
    <van-nav-bar
        title="复习单词"
        left-arrow
        @click-left="onClickLeft"
        fixed
       placeholder
    />
    <span v-show="xs1">选择您想复习的单词数量：</span>
<van-stepper v-model="num" v-show="xs1"/>
<br/>
    <van-button round color="linear-gradient(to right, #ff6034, #ee0a24)"
     @click="firstlearn" v-show="xs1">
开始复习
</van-button>

    <div class="card"> 
      <van-space>
      <p v-text="englishWord"></p>
      <van-icon name="volume-o" v-show="pron" @click="mp3play"/>
      <audio id="audio" ref="audio" class="aud" ><source :src="pron" /></audio>
      </van-space>
    </div>
    <div class="card-info">
      <p v-text="englishInstance1a"></p>
      <p v-text="chineseInstance1a"></p>
      <strong> <p style="margin-top: 23px;" v-text="chineseWorda"></p></strong>
       <!-- 选择模式按钮 -->
       <div>
        <van-space direction="vertical" :size="23" fill>
        <van-button :type="btn1" @click="changebtn(1)" style="width: 100%;height: 10vh;"  v-show="!xs3">A: {{btnA}}</van-button>
        <van-button :type="btn2"  @click="changebtn(2)" style="width: 100%;height: 10vh" v-show="!xs3">B: {{btnB}}</van-button>
        <van-button :type="btn3"  @click="changebtn(3)" style="width: 100%;height: 10vh" v-show="!xs3">C: {{btnC}}</van-button>
        <van-button :type="btn4"  @click="changebtn(4)" style="width: 100%;height: 10vh" v-show="!xs3">D: {{btnD}}</van-button>
       </van-space>
       </div>
    </div>
    <!-- 选择模式 -->
  <van-button type="primary" style="width: 88px" @click="nextword" :disabled="xs2" v-show="!xs3">下一个</van-button>
      
    <!-- 识别模式按钮 -->
    <div v-show="!xs1">
    <div class="buttons">    
        <van-space :size="18">
        <van-button type="warning" style="width: 88px" @click="showWordTranslation" :disabled="xs2" v-show="xs3">不认识</van-button>
        <van-button type="success" style="width: 88px" @click="showWordPrompt" :disabled="xs2" v-show="xs3">提示</van-button>
        <van-button type="primary" style="width: 88px" @click="getOneWord" :disabled="xs2" v-show="xs3">{{btn}}</van-button>
      </van-space>
      </div> 
  
      <van-button plain hairline type="success" class="tianjia1" @click="addnewword" :disabled="xs2" v-show="xs3">添加入单词本</van-button>
    <br/>
    </div>
 </div>
</template>

<script>
import { useRouter } from 'vue-router';
import axios from "../../utils/axios.js";
import {ref} from 'vue';
import { showToast, showSuccessToast , showFailToast } from 'vant';
import 'vant/es/toast/style';
export default {
  setup() {
    const xs1 = ref(true);
    const xs2 = ref(true);
    const xs3 = ref(true);
    const btn = ref('认识')
    const btnA=ref('');
    const btnB=ref('');
    const btnC=ref('');
    const btnD=ref('');
    const btn1=ref('default');
    const btn2=ref('default');
    const btn3=ref('default');
    const btn4=ref('default');
    const num =ref(10);
    const englishWord=ref('');
    const englishInstance1a =ref('');
    const chineseInstance1a =ref('');      
    const chineseWorda =ref('');
    const englishInstance1 =ref('');
    const chineseInstance1 =ref('');      
    const chineseWord =ref('');
    const pron =ref('');
    const count =ref('');
    const isknow =ref('');
    const router = useRouter();//实例化路由 
    let enword;
    let i =1;
    let a;
    let j =0;
    let x=true;
    //const route = useRoute(); 
    const onClickLeft = ()=> router.push('/home');  
    const mp3play = () => {		
    audio.src=pron.value;
    audio.play();		
}

  
    //不认识按钮
    const showWordTranslation = ()=>{     
      btn.value="下一个";
      if(isknow.value==0)     
      isknow.value=2;
      englishInstance1a.value=englishInstance1.value;
      chineseInstance1a.value=chineseInstance1.value;
      chineseWorda.value=chineseWord.value;
    }

   //提示按钮
    const showWordPrompt = ()=>{
      if(isknow.value==0)
      if(isknow.value!=2)  
      {isknow.value=1;}
      englishInstance1a.value=englishInstance1.value;
      chineseInstance1a.value=chineseInstance1.value;
      }
      const getDate=()=>{
    var date = new Date();
    var year = date.getFullYear(); 
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var time =  year + '-' +month + '-' + day;
    return time;
}
//认识按钮
      const getOneWord = async()=>{
        let userlearnword = {englishword:englishWord.value,count:count.value,isknow:isknow.value};
        x=true;
        btn.value="认识";
        xs2.value=true;
        let time =  getDate(); 
        //上传数据 
        await axios.post('api/Calender/CalenderByReviewword',{reviewword:1,date:time});
        await axios.post('api/UpdateUserWord',userlearnword);
        if(i>=enword.length)
       {
          englishWord.value="";
          englishInstance1a.value="";
          chineseInstance1a.value="";
          chineseWorda.value="";
          pron.value="";
          Selectmode();
          // i=1;
       }
      else
       {
        englishWord.value=enword[i].englishWord;
        englishInstance1.value="eg:"+enword[i].englishInstance1;
        chineseInstance1.value=enword[i].chineseInstance1;
        chineseWord.value=enword[i].chineseWord;  
        pron.value=enword[i].pron;
        count.value=enword[i].count;
        englishInstance1a.value="";
        chineseInstance1a.value="";
        chineseWorda.value="";
        isknow.value=0;
        count.value=count.value+1;
        xs2.value=false;
        i=i+1;
       }     
  }


    //开始学习按钮
    const firstlearn = async() => {          
      let res = await axios.post('api/GetReviewWordBy0',{num:num.value});//改为复习接口
      enword = res.data;
      //判断复习词库中剩余的词数量是否少于选择要背的单词的数量  后续添加复习模式未复习过单词/已复习单词反复复习
      if(enword.length<num.value)
      {
        if(enword.length==0)
        {
          showToast("您还未学习新词哦");    
          clearall();      
        }
        else
        {
          xs1.value=false;
          xs2.value=false;
          showToast("复习词库里你只有"+enword.length+"个单词哦，这次就背"+enword.length+"个吧");
          num.value=enword.length;
          englishWord.value=enword[0].englishWord;
          englishInstance1.value="eg:"+enword[0].englishInstance1;
          chineseInstance1.value=enword[0].chineseInstance1;
          chineseWord.value=enword[0].chineseWord;  
          pron.value=enword[0].pron;
          count.value=enword[0].count;
          isknow.value=0;
          count.value=count.value+1;
       }
      }
      else{
      xs1.value=false;
      xs2.value=false;
      englishWord.value=enword[0].englishWord;
      englishInstance1.value="eg:"+enword[0].englishInstance1;
      chineseInstance1.value=enword[0].chineseInstance1;
      chineseWord.value=enword[0].chineseWord;  
      pron.value=enword[0].pron;
      count.value=enword[0].count;
      isknow.value=0;
     count.value=count.value+1;//每复习一次+1  数据库中改为排序查找count!=0的
      }
  }

//添加入生词本
  const addnewword =async()=>{   
     if(x==true){
      let res = await axios.post('api/AddMyWord',{englishWord:englishWord.value,chineseWord:chineseWord.value});
      if(res.data.isSuccess==true)
      {
        showSuccessToast("添加成功");
        x=false;
      }
      else{showFailToast("出现错误！");}  
      }
      else{showFailToast("您已添加过本单词");}
      }

  //切换为选择模式
  const Selectmode =()=>{
      j=0;
      xs3.value=!xs3.value;
      englishWord.value=enword[j].englishWord;
      pron.value=enword[j].pron;
      xuanzhe(j);
    }

    //下一个 按钮
    const nextword = ()=>{
     
      if(j>=enword.length-1){
        showToast("已完成本轮的复习。");
        clearall();
      }
      else{ 
      j=j+1;
      englishWord.value=enword[j].englishWord;
      pron.value=enword[j].pron;
      xs2.value=true;
      btncolor();
      xuanzhe(j); 
    }
  }

    //填充ABCD选项 
    const xuanzhe =async(index)=>{
      a = Math.floor(Math.random()*(4-1)+1);
      let res = await axios.post('api/Get/Get3Random',{englishWord:englishWord.value});
      switch(a)
      {
        case 1:btnA.value=enword[index].chineseWord;
               btnB.value=res.data[0].chineseWord;
               btnC.value=res.data[1].chineseWord;
               btnD.value=res.data[2].chineseWord;break;
        case 2:btnB.value=enword[index].chineseWord;
               btnA.value=res.data[0].chineseWord;
               btnC.value=res.data[1].chineseWord;
               btnD.value=res.data[2].chineseWord;break;
        case 3:btnC.value=enword[index].chineseWord;
               btnA.value=res.data[0].chineseWord;
               btnB.value=res.data[1].chineseWord;
               btnD.value=res.data[2].chineseWord;break;
        case 4:btnD.value=enword[index].chineseWord;
               btnA.value=res.data[0].chineseWord;               
               btnB.value=res.data[1].chineseWord;
               btnC.value=res.data[2].chineseWord;break;
        default:break;
      }
    }
    //改变按钮颜色
    const changebtn =(lock)=>{
      if(a==lock){
        xs2.value=false;
        
        switch(lock){
          case 1:btn1.value="success";break;
          case 2:btn2.value="success";break;
          case 3:btn3.value="success";break;
          case 4:btn4.value="success";break;
        }
      }
      else{
        switch(lock){
          case 1:btn1.value="danger";break;
          case 2:btn2.value="danger";break;
          case 3:btn3.value="danger";break;
          case 4:btn4.value="danger";break;
        }
      }
    }
    
    //重置所有状态
    const clearall =()=>{
          xs1.value=true;
          xs2.value=true;
          xs3.value=true;
          englishWord.value="";
          englishInstance1a.value="";
          chineseInstance1a.value="";
          chineseWorda.value="";
          pron.value="";
          i=1;
          j=1;
          btncolor();
    }

    //将4个按钮样式恢复成默认
    const btncolor=()=>{
      btn1.value="default";
      btn2.value="default";
      btn3.value="default";
      btn4.value="default";
    }








    return {      
        xs1,
        xs2,
        xs3,
        btn,
        btnA,
        btnB,
        btnC,
        btnD,
        btn1,
        btn2,
        btn3,
        btn4,
        num,
        englishWord,
        englishInstance1,
        chineseWord,
        chineseInstance1,
        englishInstance1a,
        chineseWorda,
        chineseInstance1a,
        pron,
        count,
        isknow,
        onClickLeft,
        mp3play,
        showWordTranslation,
        showWordPrompt,
        getOneWord,
        firstlearn,
        addnewword,
        nextword,
        changebtn,

      };
    },
  }
</script>

<style scoped>
#word-main {
  height: 100vh;
  background-color: #f7f8fa;
}
.card {
    margin: 10px 15px 30px 15px;
    padding: 6px;
    background-color: #fff;
    border-radius: 12px;
    box-shadow: 0 8px 12px #ebedf0;
    text-align: center;
    font-size: larger;
  }
  .card-info {
    margin: 25px 15px 20px 15px;
    height: 54vh;
    padding-top: 10px;
    padding-left: 16px;
    padding-right: 16px;
    padding-bottom: 8px;
    background-color: #fff;
    border-radius: 14px;
    box-shadow: 0 20px 20px #ebedf0;
    text-align: center;
    white-space: pre-line;
  }
.buttons {

  display: flex;
  justify-content: space-between;
  margin-left: 10%;
  margin-right: 10%;

}
.tianjia1{
  width: 115px;
   height: 20px;
   margin-left:-66%;
   margin-top:10px;
}

</style>
