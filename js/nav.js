
document.addEventListener('DOMContentLoaded', (_e) => {
    document.getElementById('nav-burger-open').addEventListener('click', (e) => {
        document.getElementById('main-nav').classList = ['main-nav-open']
    })
    document.getElementById('nav-close-zone').addEventListener('click', (e) => {
        document.getElementById('main-nav').classList = ['main-nav-collapsed']
    })
    document.getElementById('nav-burger-close').addEventListener('click', (e) => {
        document.getElementById('main-nav').classList = ['main-nav-collapsed']
    })    
}, false)