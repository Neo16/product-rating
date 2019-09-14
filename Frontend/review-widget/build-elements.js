const fs = require('fs-extra');
const concat = require('concat');

(async function build() {
    const files = [
      './dist/review-widget/runtime.js',
      './dist/review-widget/es2015-polyfills.js',  
      './dist/review-widget/polyfills.js',  
      './dist/review-widget/main.js'
    ];
  
    await fs.ensureDir('elements');
    await concat(files, 'elements/review-widget.js');
  })();