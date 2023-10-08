<template>
    <router-view v-if="isRouterAlive"/>
  <van-tabbar v-model="active">
  <van-tabbar-item name="Home"  to="Home" icon="home-o" >主页</van-tabbar-item>
  <van-tabbar-item name="newword"  to="newword" icon="notes-o" >生词本</van-tabbar-item>
  <van-tabbar-item name="community"  to="community" icon="chat-o" >社区</van-tabbar-item>
  <van-tabbar-item name="user"  to="user" icon="user-circle-o" >用户</van-tabbar-item>
</van-tabbar>
</template>
<script >
import { ref } from 'vue';
import { onMounted, watch ,nextTick,provide} from "vue";
export default {
  setup() {
    const active = ref('');

    const isRouterAlive = ref(true);
    const reload = () => {
      isRouterAlive.value = false;
      nextTick(() => {
        isRouterAlive.value = true;
      });
    };
    provide("reload", reload);

    return { 
      active,
      isRouterAlive, };
  },
};
</script>