<template>
<div class="sentenceCss">
    <van-button  round plain  type="success" icon="cross" class="btnn" to="Home" size="small"></van-button>
    <div class="sentenceC">
<p>{{ sentenceEN }}</p>
<p>{{ sentenceCN }}</p>
<van-icon name="volume-o" v-show="pron" @click="mp3play"/>
<audio id="audio" ref="audio" class="aud" ><source :src="pron" /></audio>
</div>
</div>
        </template>  
        <script>  
        import { ref } from 'vue';
        import { useRouter } from 'vue-router';
        import { jsonp } from 'vue-jsonp';
        import { onMounted} from 'vue';
    import 'vant/es/toast/style'
    export default {
      setup() {
        
        const router = useRouter();//实例化路由
        const photo = ref('');
        const onClickLeft = () => router.push('/');
        const sentenceEN =ref('');
        const sentenceCN =ref('');
        const pron = ref('');
        onMounted(async() => {
          const time = getDate();
          const sentenceSrc = "https://sentence.iciba.com/index.php?c=dailysentence&m=getdetail&title="+time;
          const res =await jsonp(sentenceSrc);
          sentenceEN.value=res.content;
          sentenceCN.value=res.note;
          pron.value=res.tts;
            })

     //获得今天的日期 xxxx-xx-xx
     const getDate=()=>{
      var date = new Date();
      var year = date.getFullYear(); 
      var month = (date.getMonth() + 1)<10?"0"+(date.getMonth() + 1):(date.getMonth() + 1);
      var day = date.getDate()<10?"0"+date.getDate():date.getDate();
      var time =  year + '-' +month + '-' + day;
      return time;
  }

    const mp3play = () => {		
      audio.src=pron.value;
      audio.play();		
  }
        return {
            photo,
            sentenceEN,
            sentenceCN,
            pron,
          onClickLeft,
          mp3play,
        };
      },
    };
        </script>
        <style>

        .sentenceCss{
        margin:0px;
        height: 100vh;
        background: url(https://api.dujin.org/bing/m.php);
        background-size:100% 100%;
        background-attachment:fixed;
        }
        .sentenceC{
            position:fixed;
            bottom:22vh;
            color:aqua;
            font-size:22px;
            width: 100%;
        }
        .btnn{
  position: absolute;left: 8px;top: 16px;
}
      </style>