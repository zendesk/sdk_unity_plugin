//
//  ZendeskRequests.m
//  ZEN
//

#import "ZendeskCore.h"

#pragma mark - ZDKRequests

void _zendeskRequestsShowRequestCreation() {
    [ZDKRequests showRequestCreationWithNavController:(UINavigationController*)UnityGetGLViewController()];
}

void _zendeskRequestsShowRequestList() {
    ZendeskModalNavigationController *modalNavController = [[ZendeskModalNavigationController alloc] init];
    [ZDKRequests showRequestListWithNavController:modalNavController];
    UIViewController *rootViewController = [modalNavController.viewControllers firstObject];
    rootViewController.navigationItem.leftBarButtonItem = [[UIBarButtonItem alloc] initWithTitle:@"Close"
                                                                                           style:UIBarButtonItemStyleBordered
                                                                                          target:modalNavController
                                                                                          action:@selector(close:)];
    
    [UnityGetGLViewController() presentViewController:modalNavController animated:YES completion:nil];
}
