<template>
  <!-- 生词本页面 newword -->
  <div class="wordbj">
  <van-nav-bar title="生词本" fixed placeholder/>
  <van-sticky  :offset-top="80" position="top" >
  <van-button round type="primary" icon="plus" class="showinput1" @click="showfield" size="small" ></van-button>
  </van-sticky> 
    <div class="menb" v-for="(item,index) in state.myword" :key="item">
        <div class="mena">
          <van-swipe-cell>
          <van-space :size="20" fill>
          <p class="left">{{item.englishWord}}</p>
          <p class="a1">{{item.chineseWord}}</p>
        </van-space>
        <template #right>
    <van-button square text="删除" type="danger" class="delete-button" @click="onclick1(item,index)" />
  </template>
      </van-swipe-cell>
        </div>
   </div>
   <div>
    <br/><br/>
   <!-- 回到顶部 -->
   <van-back-top right="3vw" bottom="38vh" />

  <!-- 显示隐藏输入框按钮 -->
  

   <!-- 中英文输入框按钮 -->
   <div class="addword">
   <div v-show="showinput" >
    <van-sticky :offset-bottom="100" position="bottom"> <br/>
      <van-space direction="vertical">
      <van-cell-group inset>
        <van-field v-model="value1" label="单词" placeholder="请输入单词" />
      </van-cell-group>
      <van-cell-group inset>
        <van-field v-model="value2" label="释义" placeholder="请输入释义" />
      </van-cell-group>
    <van-button type="primary"  @click="addword" >添加</van-button>
  </van-space>
</van-sticky>
  </div>
</div>
</div>
</div>
</template>

<script>
import { ref,reactive,onMounted,inject } from 'vue';
import axios from '../../utils/axios.js'
import { showConfirmDialog , showSuccessToast , showFailToast } from 'vant';
import 'vant/es/dialog/style';
import 'vant/es/toast/style';
export default {
  setup() {
    const value1 = ref('');
    const value2 = ref('');
    const showinput =ref(false);
    const state = reactive({myword:{}});
    const refresh = inject("reload");
    onMounted(async() => {  
  let res =await axios.get('api/GetMyWord');
  state.myword=res.data;
})

    const onclick1 =(item,index)=>{
     showConfirmDialog({
     title: '确认消息',
     message:
    '是否删除单词'+item.englishWord,
})
  .then(async() => {
      let res = await axios.post('api/DeleteMyWord',item);
      if(res.data.isSuccess==true)
    { 
      refresh();
      showSuccessToast("删除成功");     
    } 
     else{showFailToast("出现错误！");}
  })
  .catch(() => {
  }); 
    };

    const addword =async()=>{
  let res = await axios.post('api/AddMyWord',{englishWord:value1.value,chineseWord:value2.value});
  if(res.data.isSuccess==true)
  {
    refresh();
    showSuccessToast("添加成功");
    value1.value="";
    value2.value="";
  }
  else{showFailToast("出现错误！");}    
}

    const showfield =()=>{
      showinput.value=!showinput.value;
    }
    return{ 
      state,
      onclick1,
      addword,
      showfield,
      refresh,
      value1,
      value2,
      showinput,
     };
  },
};
</script>

<style>
.showinput1{
  margin-left:80vw;
}
.addword{
  background-color: #f7f8fa;
}
.left{
  width:34vw;
}

.mena{
  font-size:1.1rem; 
  flex:1;
  position: relative;
  margin-left:8px;
  margin-right:8px;
}
p{
  word-wrap: break-word;
}
.mena p:nth-child(1){
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
.menb{
  height: 100%;
  display:flex;
  text-align: left;
}
.van-sticky van-sticky--fixed{
  width:100%;
}
.delete-button {
  height: 100%;
  }
  .wordbj{
  background: url(../../image/淡蓝水珠.jpg);
}
</style>