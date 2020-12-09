window.addEventListener('load', function () {
  var a = document.querySelector('a.PostLogoutRedirectUri');
  if (a) {
    setTimeout(() => (window.location = a.href), 3000);

    let remaining = 3;
    let countdown = setInterval(() => {
      let counter = document.getElementById('redirectCounter');
      remaining--;
      if (counter) {
        counter.innerHTML = remaining;
      }
      if (remaining == 0) {
        clearInterval(countdown);
      }
    }, 1000);
  }
});
