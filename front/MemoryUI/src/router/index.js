import { createRouter,createWebHashHistory } from "vue-router";
import { showFailToast } from 'vant';
const routes=[
    {path:"/",name:"first",component:()=>import("../views/firstPage/index.vue")},
    {path:"/Login",name:"Login",component:()=>import("../views/Login/index.vue")},
    {path:"/welcome",name:"welcome",component:()=>import("../views/Login/welcome.vue")},
    {path:"/register",name:"register",component:()=>import("../views/register/index.vue")},
    {path:"/Home",name:"Home",component:()=>import("../views/Home/Home.vue"),
    children:
    [
    {path:'/Home',name:"Home",component:()=>import("../views/Home/mainpage.vue")},
    {path:'/newword',name:"newword",component:()=>import("../views/Home/newword.vue")},
    {path:'/community',name:"community",component:()=>import("../views/Home/community.vue")},
    {path:'/user',name:"user",component:()=>import("../views/Home/user.vue")}
]},
{path:"/learnNew",name:"learnNew",component:()=>import("../views/recite/learnNew.vue")},
{path:"/review",name:"review",component:()=>import("../views/recite/review.vue")},

{path:"/changepwd",name:"changepwd",component:()=>import("../views/changepwd/changepwd.vue")},

]

 


const router=createRouter({history:createWebHashHistory(),routes:routes});
export default router;
//全局路由导航守卫
router.beforeEach((to,from,next) => {
    const token = localStorage.getItem('token');
    if (
      // 检查用户是否已登录
      !token &&to.name !== 'Login'&&to.name !== 'first'&&to.name !== 'register'
    ) {
      // 将用户重定向到登录页面     
            next({ name: 'first' })
            alert('用户未登陆或登录已过期，请先登录！');
    }
    else next()
  })