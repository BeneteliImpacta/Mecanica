const path = require('path');
const CopyWebpackPugin = require('copy-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const ImageMinimizerPlugin = require('image-minimizer-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const { extendDefaultPlugins } = require('svgo');

module.exports = {
    entry: {
        app: './wwwroot/src/js/app.js',
    },
    output: {
        filename: 'js/mecanicabeneteli.[name].js',
        path: path.resolve(__dirname, 'wwwroot/dist'),
        clean: true,
    },
    module: {
        rules: [
            {
                test: /\.((c|sa|sc)ss)$/i,
                use: [MiniCssExtractPlugin.loader, 'css-loader', 'postcss-loader', 'sass-loader']
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: ['babel-loader'],
            },
            {
                test: /\.(png|gif|jpe?g|svg)$/i,
                use: [
                    {
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            publicPath: '../images',
                            limit: 8192,
                        },
                    },
                ],
            },
            {
                test: /\.(eot|ttf|woff|woff2)$/,
                use: [
                    {
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            publicPath: '../fonts',
                            limit: 8192,
                        },
                    },
                ],
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: 'css/style.css',
        }),
        new ImageMinimizerPlugin({
            test: /\.(jpe?g|png|gif|svg)$/i,
            minimizerOptions: {
                // Lossless optimization with custom option
                // Feel free to experiment with options for better result for you
                plugins: [
                    ['gifsicle', { interlaced: true }],
                    ['jpegtran', { progressive: true }],
                    ['optipng', { optimizationLevel: 5 }],
                    [
                        'svgo',
                        {
                            plugins: [
                                {
                                    name: 'preset-default',
                                    params: {
                                        overrides: {
                                            // customize plugin options
                                            convertShapeToPath: {
                                                convertArcs: true,
                                            },
                                            // disable plugins
                                            convertPathData: false,
                                        },
                                    },
                                },
                            ],
                        },
                    ],
                ],
            },
        }),
        new CleanWebpackPlugin({
            verbose: true,
            cleanOnceBeforeBuildPatterns: ['*/', '!stats.json'],
        }),
        new CopyWebpackPugin({
            patterns: [
                {
                    from: './wwwroot/src/images',
                    to: 'images',
                    noErrorOnMissing: true
                },
                {
                    from: './wwwroot/src/fonts',
                    to: 'fonts'
                }
                //,{
                //    from: './wwwroot/src/css',
                //    to: 'css'
                //}
            ]
        })
    ],
    target: 'web'
};