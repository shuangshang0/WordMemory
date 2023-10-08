<template>
  <div class="denglubeijing">
    <div class="bg2">
    <p>欢迎来到单词记忆应用系统！</p>
    <br/>
    <van-button type="primary" size="large" to="Login" class="btn">用户登录</van-button>
    <br/><br/><br/>
    <van-button plain hairline type="primary" size="large" to="register" class="btn">用户注册</van-button>
    <br/><br/>
    <p class="xiaozi">日后你所有的成就，都将得益于今天一点一滴的努力。别放弃，所有的困境都不足以拦住你前进的方向。</p>
  </div>
  </div>
</template>
<style>
.btn{
  width: 300px;
}
.xiaozi{
  margin-right:3vw;
  margin-left:3vw;
  font-size:16px;
  color:black;
}
.bg2{
        position:fixed;
        bottom:23vh;
        width: 100%;
        margin:auto;
        color:#E6E8FA;
        font-size:24px;
      }
.denglubeijing{
        margin:0px;
        width: 100vw;
        height: 100vh;
        background: url(../../image/星空.jpg);
        background-size:100% 100%;
        background-attachment:fixed;
      }
</style>