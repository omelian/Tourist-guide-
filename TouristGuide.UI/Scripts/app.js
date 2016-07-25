var app = angular.module('institutionApp', ['ngRoute', 'ngAnimate', 'route-segment', 'view-segment', 'ui.bootstrap', 'ngMap', 'angularUtils.directives.dirPagination', 'ngCookies', 'ngModal', 'ngMaterial','angular-toArrayFilter','ngMessages']);

app.config(function ($routeSegmentProvider, $routeProvider) {

    $routeSegmentProvider

        .when('/restaurant/:profileId', 'rest.news')
        .when('/restaurant/:profileId/news', 'rest.news')
        .when('/restaurant/:profileId/menu', 'rest.menu')
        .when('/restaurant/:profileId/photo', 'rest.photo')
        .when('/restaurant/:profileId/comments', 'rest.comments')
        .when('/restaurant/:profileId/moderators', 'rest.moderators')
    
        .when('/sightseeing/:profileId', 'sightseeing.news')
        .when('/sightseeing/:profileId/news', 'sightseeing.news')
        .when('/sightseeing/:profileId/photo', 'sightseeing.photo')
        .when('/sightseeing/:profileId/comments', 'sightseeing.comments')
        .when('/sightseeing/:profileId/information', 'sightseeing.information')
        .when('/sightseeing/:profileId/poster', 'sightseeing.poster')
        .when('/sightseeing/:profileId/moderators', 'sightseeing.moderators')

        .when('/leisure/:profileId', 'leisure.news')
        .when('/leisure/:profileId/news', 'leisure.news')
        .when('/leisure/:profileId/photo', 'leisure.photo')
        .when('/leisure/:profileId/comments', 'leisure.comments')
        .when('/leisure/:profileId/information', 'leisure.information')
        .when('/leisure/:profileId/moderators', 'leisure.moderators')

        .when('/main', 'main')
        .when('/sightseeingList', 'sightseeingList')
        .when('/restaurantList', 'restaurantList')
        .when('/leisureList', 'leisureList')
        

        .segment('main', {
            templateUrl: '/app/views/main.html'
        })
  
       .segment('sightseeingList', {
           templateUrl: '/app/views/listpage_sightseeing.html'
       })

        .segment('restaurantList', {
            templateUrl: '/app/views/listpage_restaurant.html'
        })

        .segment('leisureList', {
            templateUrl: '/app/views/listpage_leisure.html'
            })
    

        .segment('rest', {
            templateUrl: '/app/views/restaurant/restaurant.html'
        })

        .within()

            .segment('news', {
                'default': true,
                templateUrl: '/app/views/restaurant/restaurant-items/news.html'
            })

            .segment('menu', {
                templateUrl: '/app/views/restaurant/restaurant-items/menu.html',
                controller: 'MenuPageController'
        
            })


            .segment('place', {
                templateUrl: '/app/views/restaurant/restaurant-items/place.html'

            })
            .segment('comments', {
                templateUrl: '/app/views/restaurant/common-items/comments.html'
            })
            .segment('photo', {
                templateUrl: '/app/views/restaurant/restaurant-items/photo.html',
                controller: 'PhotoPageController'

            })
            .segment('moderators', {
                templateUrl: '/app/views/restaurant/common-items/moderators.html'

            })

        .up()

    .segment('sightseeing', {
        templateUrl: '/app/views/restaurant/sightseeing.html'
    })

        .within()

            .segment('news', {
                'default': true,
                templateUrl: '/app/views/restaurant/restaurant-items/news.html'
            })

            .segment('comments', {
                templateUrl: '/app/views/restaurant/common-items/comments.html'
            })
            .segment('photo', {
                templateUrl: '/app/views/restaurant/restaurant-items/photo.html',
                controller: 'PhotoPageController'

            })
        .segment('information', {
            templateUrl: '/app/views/restaurant/sightseeing-items/information.html'
           

        })
        .segment('poster', {
            templateUrl: '/app/views/restaurant/sightseeing-items/poster.html'


        })
        .segment('moderators', {
            templateUrl: '/app/views/restaurant/common-items/moderators.html'

        })
        .up()

    .segment('leisure', {
        templateUrl: '/app/views/restaurant/leisure.html'
    })

        .within()

            .segment('news', {
                'default': true,
                templateUrl: '/app/views/restaurant/sightseeing-items/news.html'
            })

            .segment('comments', {
                templateUrl: '/app/views/restaurant/common-items/comments.html'
            })
            .segment('photo', {
                templateUrl: '/app/views/restaurant/sightseeing-items/photo.html',
                controller: 'PhotoPageController'

            })
        .segment('information', {
            templateUrl: '/app/views/restaurant/common-items/information.html'
           

        })
        .segment('moderators', {
            templateUrl: '/app/views/restaurant/common-items/moderators.html'

        })

        .up()

       
    $routeProvider.otherwise({ redirectTo: '/main' });
    
});
