// 取得命令列傳入的第一個參數並轉為數字
const ver = process.argv[2] ? parseInt(process.argv[2]) : 1;

function getGeneralizedAbsoluteTheory(ver) {
  switch (ver) {
    case 1:
      return "沒有奇蹟只有累積 一分耕耘一分收穫 古今中外皆然 !";
    case 2:
      return "沒有奇蹟只有累積 財富不滅錢錢守恒 古今中外皆然 !!";
    default:
      return "無[廣義絕對論 v" + ver + "]版本 !";
  }
}

console.log(getGeneralizedAbsoluteTheory(ver));