/// <binding BeforeBuild='before-build, cssmin:all, uglify' AfterBuild='after-build' Clean='clean' ProjectOpened='project-open' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        bower: {
            install: {
                options: {
                    targetDir: "wwwroot/lib",
                    layout: "byComponent",
                    cleanTargetDir: true
                }
            },
        },
        cssmin: {
            all: {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/css',
                    src: ['*.css', '!*.min.css'],
                    dest: 'wwwroot/css',
                    ext: '.min.css'
                }]
            }
        },
        uglify: {
            all: {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/js',
                    src: ['*.js', '!*.min.js'],
                    dest: 'wwwroot/js',
                    ext: '.min.js'
                }]
            }
        }
    });

    grunt.loadNpmTasks("grunt-bower-task");
    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks("grunt-contrib-uglify");

    grunt.registerTask("before-build", ["bower:install", "cssmin:all", "uglify:all"]);
    grunt.registerTask("after-build", []);
    grunt.registerTask("clean", []);
    grunt.registerTask("project-open", []);
};