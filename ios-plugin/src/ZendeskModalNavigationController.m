

#import "ZendeskModalNavigationController.h"

@interface ZendeskModalNavigationController ()

@end

@implementation ZendeskModalNavigationController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    [self setModalPresentationStyle:UIModalPresentationFormSheet];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

-(void)close:(id)sender {
    [self dismissViewControllerAnimated:TRUE completion:nil];
}

@end
