const fs = require('fs-extra');
const concat = require('concat');

//encapsulate all angular code to one .js file 
(async function build() {
  const files = [
    './dist/product-widget/runtime.js',
    './dist/product-widget/es2015-polyfills.js',  
    './dist/product-widget/polyfills.js',  
    './dist/product-widget/main.js'
  ];

  await fs.ensureDir('elements');
  await concat(files, 'elements/score-widget.js');
})();