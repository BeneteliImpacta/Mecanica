const { merge } = require('webpack-merge');

const webpackConfiguration = require('./webpack.config');

module.exports = merge(webpackConfiguration, {
    mode: 'development',

    /* Manage source maps generation process */
    devtool: 'eval-source-map',

    /* Additional plugins configuration */
    plugins: [],
});
