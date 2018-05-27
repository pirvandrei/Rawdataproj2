
define(['knockout', 'request'], function (ko, req) {
    return function (params) {
        var test = ko.observable('sdfsd');
        var self = this;
        var questionData = ko.observableArray();
        var answerData = ko.observableArray();
        
        //First we need to load question data
        var updateValue = function ()
        {   
            test('nowa')
        }
    ko.computed(function () {
        req.getQuestion(params, function (data) {
                questionData(data);
                answerData(data.answers);
                console.log(questionData());
            test('niet');
            });
        });
    return {
            questionData,
            answerData,test,updateValue
        };
    };
});
