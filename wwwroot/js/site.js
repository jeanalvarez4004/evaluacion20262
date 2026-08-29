document.addEventListener('DOMContentLoaded',()=>{
 const c=document.getElementById('counter1');
 if(c){ let n=0; const t=127; const i=setInterval(()=>{ n+=3; if(n>=t){n=t;clearInterval(i)} c.textContent=n+'+'; },28);}
 document.querySelectorAll('.service-card').forEach(el=>{
  el.addEventListener('mousemove',e=>{
   const r=el.getBoundingClientRect();
   const x=e.clientX-r.left, y=e.clientY-r.top;
   el.style.transform=`translateY(-6px) perspective(600px) rotateY(${(x/r.width-.5)*6}deg) rotateX(${-(y/r.height-.5)*6}deg)`;
  });
  el.addEventListener('mouseleave',()=> el.style.transform='');
 });
});
