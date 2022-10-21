angular.module("umbraco").controller("CustomWelcomeDashboardController", function ($scope, userService, logResource, entityResource) {
    var vm = this;
    vm.UserName = "guest";
    var baseUrl = 'https://localhost:44345/Client/createclient';

    var user = userService.getCurrentUser().then(function (user) {
        console.log(user);
        vm.UserName = user.name;
    });
    var req = {
        method: 'POST',
        url: baseUrl,
        headers: {
            'Content-Type': Text
        },
        params: { name: 'name' },
        
    }

});