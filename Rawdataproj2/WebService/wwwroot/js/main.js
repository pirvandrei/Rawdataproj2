(function () {
    console.log('test')

    require.config({
        baseUrl: 'bower_components',
        paths: {
            knockout: 'knockout/dist/knockout',
            jquery: 'jQuery/dist/jquery.min',
            text: 'requirejs-text/text',
            jqcloud: 'jqcloud2/dist/jqcloud'
        }
    });
})();