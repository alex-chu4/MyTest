-----------------------------------------------
Docker

docker build -t imagechat3-1 .
docker build --no-cache --progress=plain -t imagechat3-1 .

docker run -it --rm -p 5508:8080 --name imagechat3-1 imagechat3-1:latest
http://localhost:5508/index.html

docker system df
docker builder prune
-----------------------------------------------

-----------------------------------------------
Git 

git add . && git commit --amend --no-edit
git log --oneline --graph 
git push -u -f MyTest master
-----------------------------------------------

-----------------------------------------------
Render

設定:
Source Code :               MyTest
Name:                       MyTest3
Language:                   Docker
Branch:                     master
Region:                     新加坡
Root Directory:             ImageChat3-1
Instance Type（方案規格）： 下拉拉到最下面，選擇 「Free」（免費方案）

Runtime URL:
https://mytest3-xxme.onrender.com/index.html
-----------------------------------------------
